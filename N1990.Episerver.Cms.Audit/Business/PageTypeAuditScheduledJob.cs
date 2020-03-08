using System;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using System.Linq;
using N1990.Episerver.Cms.Audit.Models;
using EPiServer.DataAbstraction;
using Newtonsoft.Json;

namespace N1990.Episerver.Cms.Audit.Business
{
    [ScheduledPlugIn(DisplayName = "Page Type Audit Job")]
    public class PageTypeAuditScheduledJob : ScheduledJobBase
    {
        private bool _stopSignaled;

        public PageTypeAuditScheduledJob()
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
            OnStatusChanged(String.Format("Investigating usage of Page Types"));

            //Add implementation
            var cmsAuditor = ServiceLocator.Current.GetInstance<ICmsAuditor>();

            var pageTypes = cmsAuditor.GetContentTypesOfType<PageType>();

            int usagesFound = 0;
            PageTypeUsagesData.CleanUp();

            foreach (var pageType in pageTypes)
            {
                var audit = cmsAuditor.GenerateContentTypeAudit(pageType.ContentTypeId, false, true);

                var pageTypeUsage = new PageTypeUsagesData
                {
                    ContentTypeId = audit.ContentTypeId,
                    AuditJson = JsonConvert.SerializeObject(audit)
                };

                PageTypeUsagesData.Save(pageTypeUsage);

                usagesFound += audit.Usages.Count();
                OnStatusChanged(String.Format("Done with {0}", audit.Name));

                if (_stopSignaled)
                {
                    return "Job was cancelled";
                }
            }

            return string.Format("Done looking through content. Found {0} page types used {1} time(s)",
                pageTypes.Count(), usagesFound);
        }
    }
}
