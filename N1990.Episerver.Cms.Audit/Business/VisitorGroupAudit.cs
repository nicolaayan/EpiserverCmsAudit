using System;
using EPiServer;
using EPiServer.Core;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using System.Linq;
using N1990.Episerver.Cms.Audit.Models;
using EPiServer.Security;

namespace N1990.Episerver.Cms.Audit.Business
{
    [ScheduledPlugIn(DisplayName = "Audience Audit Job")]
    public class VisitorGroupAudit : ScheduledJobBase
    {
        private bool _stopSignaled;

        public VisitorGroupAudit()
        {
            IsStoppable = true;
        }

        /// <summary>
        /// Called when a user clicks on Stop for a manually started job, or when ASP.NET shuts down.
        /// </summary>
        public override void Stop()
        {
            _stopSignaled = true;
        }

        /// <summary>
        /// Called when a scheduled job executes
        /// </summary>
        /// <returns>A status message to be stored in the database log and visible from admin mode</returns>
        public override string Execute()
        {
            //Call OnStatusChanged to periodically notify progress of job for manually started jobs
            OnStatusChanged(String.Format("Investigating usage of Audiences"));

            //Add implementation
            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            var vgrepo = ServiceLocator.Current.GetInstance<IVisitorGroupRepository>();
            var visitorgrouplist=vgrepo.List().ToList();
            int cnt = 0;
            int usesfound = 0;
            VisitorGroupUse.CleanUp();

            //Add implementation
            foreach (var cr in repo.GetDescendents(ContentReference.RootPage))
            {
                var c = repo.Get<IContentData>(cr);
                if (c is ISecurable)
                {
                    var sec=(c as ISecurable).GetSecurityDescriptor();
                    if(sec is ContentAccessControlList)
                    {
                        var cacl = (sec as ContentAccessControlList);
                        foreach(var ca in cacl.Where(cac => cac.Value.EntityType == SecurityEntityType.VisitorGroup))
                        {
                            usesfound++;
                            VisitorGroupUse vgu = new VisitorGroupUse();
                            vgu.Seen = DateTime.Now;
                            vgu.Content = cr;
                            vgu.VisitorGroup = visitorgrouplist.Where(vvg => vvg.Name == ca.Value.Name).Select(vvg => vvg.Id.ToString()).FirstOrDefault();
                            vgu.PropertyName = "(Content Access Rights)";
                            vgu.ContentName = (c as IContent).Name;
                            vgu.ContentType = (c is PageData) ? "Page" : (c is BlockData) ? "Block" : "Other";
                            VisitorGroupUse.Save(vgu);
                        }
                    }
                    //Look for EntityType="VisitorGroup" and then match on name?!
                }
                foreach (var p in c.Property)
                {
                    if (p.Value == null) continue;
                    if (p.PropertyValueType == typeof(ContentArea))
                    {
                        var ca = p.Value as ContentArea;
                        if (ca == null) continue;
                        foreach (var f in ca.Items.Where(l => l.AllowedRoles != null && l.AllowedRoles.Any()))
                        {
                            //Match! This page uses the audiences in l.AllowedRoles. Record.
                            foreach (var r in f.AllowedRoles)
                            {
                                usesfound++;
                                VisitorGroupUse vgu = new VisitorGroupUse();
                                vgu.Seen = DateTime.Now;
                                vgu.VisitorGroup = r;
                                vgu.Content = cr;
                                vgu.PropertyName = p.Name;
                                vgu.ContentName = (c as IContent).Name;
                                vgu.ContentType = (c is PageData) ? "Page" : (c is BlockData) ? "Block" : "Other";
                                VisitorGroupUse.Save(vgu);
                            }
                        }
                    }
                    else if (p.PropertyValueType == typeof(XhtmlString))
                    {
                        var ca = p.Value as XhtmlString;
                        if (ca == null) continue;
                        foreach (var f in ca.Fragments.Where(fr => fr is EPiServer.Core.Html.StringParsing.PersonalizedContentFragment))
                        {
                            
                            var j = f as EPiServer.Core.Html.StringParsing.PersonalizedContentFragment;
                            var roles = j.GetRoles();
                            foreach (var r in roles)
                            {
                                usesfound++;
                                VisitorGroupUse vgu = new VisitorGroupUse();
                                vgu.Seen = DateTime.Now;
                                vgu.VisitorGroup = r;
                                vgu.Content = cr;
                                vgu.PropertyName = p.Name;
                                vgu.ContentName = (c as IContent).Name;
                                vgu.ContentType = (c is PageData) ? "Page" : (c is BlockData) ? "Block" : "Other";
                                VisitorGroupUse.Save(vgu);
                            }
                        }

                    }
                }
                cnt++;
                OnStatusChanged(String.Format("Done with {0}", cnt));

                if (_stopSignaled)
                {
                    return "Job was cancelled";
                }
            }


            return string.Format("Done looking through content. Found {0} uses of audiences in {1} content elements",usesfound,cnt);
        }
    }
}
