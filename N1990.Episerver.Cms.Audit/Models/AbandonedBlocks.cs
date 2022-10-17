using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;

namespace N1990.Episerver.Cms.Audit.Models
{
	public class AbandonedBlocks
    {
		public List<ContentReferenceDetails> ContentReferences { get; set; } = new List<ContentReferenceDetails>();
        
        public DateTime JobLastRunTime { get; set; }

    }
}