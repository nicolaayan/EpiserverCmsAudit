using System;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Web;

namespace N1990.Episerver.Cms.Audit.Models
{
    public class ContentReferenceDetails
    {
        public string Name;
        public ContentReference ContentLink;
        public bool IsSite => SiteId != null;
        public Guid SiteId;
        public ContentReferenceDetails() { }

        public ContentReferenceDetails(ContentItem item)
        {
            ContentLink = item.ContentLink;
            Name = item.Name;
            SiteId = item.SiteId;
        }

        public ContentReferenceDetails(ReferenceInformation rtc, ISiteDefinitionResolver siteDefinitionResolver)
        {
            Name = rtc.OwnerName;
            ContentLink = rtc.OwnerID;
            SiteId = siteDefinitionResolver.GetByContent(rtc.OwnerID, true)?.Id ?? Guid.Empty;
        }
    }
}