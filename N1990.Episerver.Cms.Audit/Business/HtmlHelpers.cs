using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Business
{
    public static class HtmlHelpers
    {
        public static HtmlString RenderBreadcrumbs(this IHtmlHelper helper, 
            ContentTypeAudit.ContentItem contentItem)
        {
            var htmlString = "<a href =\"" + UrlResolver.Current.GetUrl(contentItem.ContentLink) + "\" target=\"_blank\">" + contentItem.Name + "</a>";
            while (contentItem.Parent != null)
            {
                contentItem = contentItem.Parent;
                htmlString = (contentItem.Parent == null ? "Home" : contentItem.Name) + " > " + htmlString;
            }

            return new HtmlString(htmlString);
        }
    }
}