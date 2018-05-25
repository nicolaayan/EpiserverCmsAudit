using System.Collections.Generic;

namespace N1990.Episerver.Cms.Audit.Models
{
	public class CmsAuditPage
	{
		public List<SiteAudit> Sites { get; set; }
        public List<ContentTypeAudit> ContentTypes { get; set; }

		public CmsAuditPage()
		{
			Sites = new List<SiteAudit>();
		}
		

	}
}