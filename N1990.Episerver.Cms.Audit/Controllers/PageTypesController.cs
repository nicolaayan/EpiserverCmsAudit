using System;
using System.Linq;
using System.Web.Mvc;
using EPiServer.DataAbstraction;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "WebEditors,WebAdmins,Administrators")]
    public class PageTypesController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public PageTypesController(ICmsAuditor cmsAuditor)
	    {
	        _cmsAuditor = cmsAuditor;
	    }

        public ActionResult Index()
        {
            var model = new CmsAuditPage();

            var pageTypes = _cmsAuditor.GetContentTypesOfType<PageType>();

            model.ContentTypes = pageTypes;

            return View(model);
        }

        public ActionResult PageTypeAudit(int contentTypeId)
        {
            var model = _cmsAuditor.GenerateContentTypeAudit(contentTypeId, false, true);
            return View(model);
        }
    }
}