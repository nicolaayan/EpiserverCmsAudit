using EPiServer.DataAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "AuditAdmins")]
    public class PageTypesController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public PageTypesController(ICmsAuditor cmsAuditor)
	    {
	        _cmsAuditor = cmsAuditor;
	    }

        [Route("/EPiServer/cmsaudit/pagetypes")]
        public ActionResult Index()
        {
            var model = new CmsAuditPage();
            model.ContentTypes = _cmsAuditor.GetContentTypesOfType<PageType>();
            model.JobLastRunTime = _cmsAuditor.JobLastRunTime<PageTypeAuditScheduledJob>();

            return View(model);
        }

        public ActionResult RunJob()
        {
            _cmsAuditor.JobStartManually<PageTypeAuditScheduledJob>();
            return RedirectToAction("Index");
        }

        public ActionResult PageTypeAudit(int contentTypeId)
        {
            var model = _cmsAuditor.GetPageTypeAudit(contentTypeId);
            return View(model);
        }
    }
}