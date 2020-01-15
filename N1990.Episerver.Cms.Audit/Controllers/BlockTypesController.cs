using System;
using System.Linq;
using System.Web.Mvc;
using EPiServer.DataAbstraction;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "WebEditors,WebAdmins,Administrators")]
    public class BlockTypesController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public BlockTypesController(ICmsAuditor cmsAuditor)
	    {
	        _cmsAuditor = cmsAuditor;
        }

        public ActionResult Index()
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
    }
}