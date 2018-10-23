using System;
using System.Collections.Generic;
using EPiServer.Web;
using N1990.Episerver.Cms.Audit.Models;

namespace N1990.Episerver.Cms.Audit.Business
{
    public interface ICmsAuditor
    {
        List<ContentTypeAudit> GetContentTypesOfType<T>();

        List<VGAudit> GetVisitorGroups();
        DateTime VGJobLastRunTime();
        void VGJobStartManually();

        List<ContentTypeAudit> GetContentItemsOfTypes(List<ContentTypeAudit> contentTypes,
            bool includeReferences, bool includeParentDetail);

        void PopulateContentItemsOfType(ContentTypeAudit contentTypeAudit,
            bool includeReferences, bool includeParentDetail);

        ContentTypeAudit GetContentTypeAudit(int contentTypeId, bool includeReferences, bool includeParentDetail);

        SiteAudit GetSiteAudit(Guid siteGuid);

        List<SiteDefinition> GetSiteDefinitions();
    }
}