using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N1990.Episerver.Cms.Audit.Business;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Controllers
{
    [Authorize(Roles = "AuditAdmins")]
    public class CmsAuditController : Controller
    {
        private readonly ICmsAuditor _cmsAuditor;

        public CmsAuditController(ICmsAuditor cmsAuditor)
	    {
	        _cmsAuditor = cmsAuditor;
	    }

        [Route("/EPiServer/cmsaudit/sites")]
		public ActionResult Index()
		{
			var model = new CmsAuditPage()
			{
				Sites = _cmsAuditor.GetSiteDefinitions()
                    .Where(sd => sd.Id != Guid.Empty)
                    .Select(sd => new SiteAudit
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
    }
}