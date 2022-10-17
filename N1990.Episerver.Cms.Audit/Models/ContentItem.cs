using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace N1990.Episerver.Cms.Audit.Models
{
    public partial class ContentItem
    {
        public Guid SiteId { get; set; }
 
        public string Name { get; set; }
        
        public ContentReference ContentLink { get; set; }
        
        public ContentItem Parent { get; set; }

        public List<ContentReferenceDetails> ParentReferences { get; set; }

        public ContentItem()
        {
            ParentReferences = new List<ContentReferenceDetails>();
        }
    }
}