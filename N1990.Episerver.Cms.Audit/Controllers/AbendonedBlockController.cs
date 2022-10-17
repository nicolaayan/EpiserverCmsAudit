using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer.DataAbstraction;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "AuditAdmins")]
    public class AbendonedBlockController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public AbendonedBlockController(ICmsAuditor cmsAuditor)
        {
            _cmsAuditor = cmsAuditor;
        }

        public ActionResult Index()
        {
            //var model = new CmsAuditPage();
            //model.ContentTypes = _cmsAuditor.GetContentTypesOfType<BlockType>();
            //model.JobLastRunTime = _cmsAuditor.JobLastRunTime<BlockTypeAudit>();

            //return View(model);

            return FullAbandonedBlockAudit(); // ToDo: if to slow split up
        }

        public ActionResult RunJob()
        {
            _cmsAuditor.JobStartManually<BlockTypeAudit>();
            return RedirectToAction("Index");
        }

        public ActionResult FullAbandonedBlockAudit()
        {
            var contentTypes = _cmsAuditor.GetContentTypesOfType<BlockType>();

            var data = contentTypes.Select(x => _cmsAuditor.GetBlockTypeAudit(x.ContentTypeId)) // ToDo: if to slow split up
                                   .SelectMany(x => x.Usages)
                                   .Where(x => x.ParentReferences.Count == 0)
                                   .Select(x => new ContentReferenceDetails(x))
                                   .ToList();

            var model = new AbandonedBlocks
            {
                ContentReferences = data,
                JobLastRunTime = _cmsAuditor.JobLastRunTime<BlockTypeAudit>()
            };

            return View(model);
        }
    }
}