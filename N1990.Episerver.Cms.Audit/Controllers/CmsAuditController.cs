using System;
using System.Linq;
using System.Web.Mvc;
using EPiServer.DataAbstraction;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "WebEditors,WebAdmins,Administrators")]
    public class CmsAuditController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public CmsAuditController(ICmsAuditor cmsAuditor)
	    {
	        _cmsAuditor = cmsAuditor;
	    }

		public ActionResult Index()
		{
			var model = new CmsAuditPage()
			{
				Sites = _cmsAuditor.GetSiteDefinitions().Select(sd => new SiteAudit
				{
                    SiteDefo = sd
				}).ToList()
			};
            return View(model);
        }

        public ActionResult IndexSiteAudit(Guid siteGuid)
        {
            var model = _cmsAuditor.GetSiteAudit(siteGuid);
            return View(model);
        }

        public ActionResult PageTypes()
        {
            var model = new CmsAuditPage();

            var pageTypes = _cmsAuditor.GetContentTypesOfType<PageType>();

            model.ContentTypes = pageTypes;

            return View(model);
        }

        public ActionResult PageTypeAudit(int contentTypeId)
        {
            var model = _cmsAuditor.GetContentTypeAudit(contentTypeId, false, true);
            return View(model);
        }

        public ActionResult BlockTypes()
        {
            var model = new CmsAuditPage();
            model.ContentTypes = _cmsAuditor.GetContentTypesOfType<BlockType>();
            
            return View(model);
        }

        public ActionResult BlockTypeAudit(int contentTypeId)
        {
            var model = _cmsAuditor.GetContentTypeAudit(contentTypeId, true, false);
            return View(model);
        }

        public ActionResult VisitorGroups()
        {
            var model = new CmsAuditPage();
            model.VisitorGroups = _cmsAuditor.GetVisitorGroups().OrderBy(v => v.Name).ToList();
            model.VGLastRunTime = _cmsAuditor.VGJobLastRunTime();
            return View(model);
        }

        public ActionResult RunVGJob()
        {
            _cmsAuditor.VGJobStartManually();
            return RedirectToAction("VisitorGroups");
        }

        public ActionResult VisitorGroupAudit(string visitorGroupID)
        {
            return View();
        }
    }
}