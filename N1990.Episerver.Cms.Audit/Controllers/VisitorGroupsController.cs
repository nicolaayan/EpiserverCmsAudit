using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "AuditAdmins")]
    public class VisitorGroupsController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public VisitorGroupsController(ICmsAuditor cmsAuditor)
	    {
	        _cmsAuditor = cmsAuditor;
	    }
        
        public ActionResult Index()
        {
            var model = new CmsAuditPage();
            model.VisitorGroups = _cmsAuditor.GetVisitorGroups().OrderBy(v => v.Name).ToList();
            model.JobLastRunTime = _cmsAuditor.JobLastRunTime<VisitorGroupAudit>();
            return View(model);
        }

        public ActionResult RunVGJob()
        {
            _cmsAuditor.JobStartManually<VisitorGroupAudit>();
            return RedirectToAction("Index");
        }

        public ActionResult VisitorGroupAudit(string visitorGroupID)
        {
            return View();
        }
    }
}