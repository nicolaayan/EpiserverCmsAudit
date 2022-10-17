using System.Web;
using System.Web.Mvc;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Business
{
    public static class HtmlHelpers
    {
        public static IHtmlString RenderBreadcrumbs(this HtmlHelper helper, ContentItem contentItem)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlHelper>();
            var htmlString = "<a href =\"" + urlResolver.ContentUrl(contentItem.ContentLink) 
                                           + "\" target=\"_blank\">" + contentItem.Name + "</a>";
            while (contentItem.Parent != null)
            {
                contentItem = contentItem.Parent;
                htmlString = (contentItem.Parent == null ? "Home" : contentItem.Name) + " > " + htmlString;
            }

            return new MvcHtmlString(htmlString);
        }
    }
}