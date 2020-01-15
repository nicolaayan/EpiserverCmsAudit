using System;
using System.Linq;
using System.Web.Mvc;
using EPiServer.DataAbstraction;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "WebEditors,WebAdmins,Administrators")]
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
            model.VGLastRunTime = _cmsAuditor.VGJobLastRunTime();
            return View(model);
        }

        public ActionResult RunVGJob()
        {
            _cmsAuditor.VGJobStartManually();
            return RedirectToAction("Index");
        }

        public ActionResult VisitorGroupAudit(string visitorGroupID)
        {
            return View();
        }
    }
}