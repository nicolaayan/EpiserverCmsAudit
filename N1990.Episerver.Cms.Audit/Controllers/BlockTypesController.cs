using EPiServer.DataAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "AuditAdmins")]
    public class BlockTypesController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public BlockTypesController(ICmsAuditor cmsAuditor)
	    {
	        _cmsAuditor = cmsAuditor;
        }

        [Route("/EPiServer/cmsaudit/blocktypes")]
        public ActionResult Index()
        {
            var model = new CmsAuditPage();
            model.ContentTypes = _cmsAuditor.GetContentTypesOfType<BlockType>();
            model.JobLastRunTime = _cmsAuditor.JobLastRunTime<BlockTypeAudit>();
            return View(model);
        }

        public ActionResult RunJob()
        {
            _cmsAuditor.JobStartManually<BlockTypeAudit>();
            return RedirectToAction("Index");
        }

        public ActionResult BlockTypeAudit(int contentTypeId)
        {
            var model = _cmsAuditor.GetBlockTypeAudit(contentTypeId);
            return View(model);
        }
    }
}