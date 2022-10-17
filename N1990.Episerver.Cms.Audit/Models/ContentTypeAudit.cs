using System.Collections.Generic;
using System.Linq;

namespace N1990.Episerver.Cms.Audit.Models
{
	public class ContentTypeAudit
	{
	    public int ContentTypeId { get; set; }
		public string Name { get; set; }
		public string FullName { get; set; }
		public string Namespace { get; set; }
		public List<ContentItem> Usages { get; set; }

        #region Dynamic Properties

        public string UsagesSummary =>
	        $"{Usages.Select(u => u.SiteId).Distinct().ToList().Count} site(s), {Usages.Count} page(s)";

	    public string UsagesExpandButton => Usages.Count > 0
	        ? "<button class=\"btn btn-xs btn-info\" onclick=\"toggleNextHiddenTr(this)\"><i class=\"glyphicon glyphicon-collapse-down\"></i></button>"
	        : "";

        #endregion

        public ContentTypeAudit()
		{
			Usages = new List<ContentItem>();
		}
	}
}