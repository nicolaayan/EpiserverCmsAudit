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
    [ScheduledPlugIn(DisplayName = "Block Type Audit Job")]
    public class BlockTypeAudit : ScheduledJobBase
    {
        private bool _stopSignaled;

        public BlockTypeAudit()
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
            OnStatusChanged(String.Format("Investigating usage of Block Types"));

            //Add implementation
            var cmsAuditor = ServiceLocator.Current.GetInstance<ICmsAuditor>();

            var blockTypes = cmsAuditor.GetContentTypesOfType<BlockType>();

            int usesfound = 0;
            BlockTypeUse.CleanUp();

            foreach (var blockType in blockTypes)
            {
                var audit = cmsAuditor.GenerateContentTypeAudit(blockType.ContentTypeId, true, false);

                var blockTypeUse = new BlockTypeUse
                {
                    ContentTypeId = audit.ContentTypeId,
                    AuditJson = JsonConvert.SerializeObject(audit)
                };

                BlockTypeUse.Save(blockTypeUse);

                usesfound += audit.Usages.Count();
                OnStatusChanged(String.Format("Done with {0}", audit.FullName));

                if (_stopSignaled)
                {
                    return "Job was cancelled";
                }
            }
            return string.Format("Done looking through content. Found {0} block types used {1} time(s)",blockTypes.Count(),usesfound);
        }
    }
}
