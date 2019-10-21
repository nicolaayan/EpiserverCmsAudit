using System.Collections.Generic;
using EPiServer.Framework.Localization;
using EPiServer.Security;
using EPiServer.Shell;
using EPiServer.Shell.Navigation;

namespace N1990.Episerver.Cms.Audit.Business
{
    [MenuProvider]
    public class AuditMenuProvider : IMenuProvider
    {
        private readonly LocalizationService _localizationService;

        public AuditMenuProvider(LocalizationService localizationService)
        {
            this._localizationService = localizationService;
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            const string menuPath = "/global/cms/audit";
            var menuText = _localizationService.GetString("/cmsaudit/menus/audit", "Audit");

            // Register menu items for each controller action but with the same path 
            // key to ensure the menu displays in the new Episerver UI
            var menuIndex =
                new UrlMenuItem(
                    menuText,
                    menuPath,
                    Paths.ToResource("CmsAudit", "")) 
                {
                    IsAvailable = (request) => true,
                    SortIndex = int.MaxValue
                };

            var menuPageTypes =
                new UrlMenuItem(
                    menuText,
                    menuPath,
                    Paths.ToResource("CmsAudit", "CmsAudit/PageTypes"))
                {
                    IsAvailable = (request) => false,
                    SortIndex = int.MaxValue
                };

            var menuBlockTypes =
                new UrlMenuItem(
                    menuText,
                    menuPath,
                    Paths.ToResource("CmsAudit", "CmsAudit/BlockTypes"))
                {
                    IsAvailable = (request) => false,
                    SortIndex = int.MaxValue
                };

            var menuVisitorGroups =
                new UrlMenuItem(
                    menuText,
                    menuPath,
                    Paths.ToResource("CmsAudit", "CmsAudit/VisitorGroups"))
                {
                    IsAvailable = (request) => false,
                    SortIndex = int.MaxValue
                };

            var list = new List<MenuItem>();
            list.Add(menuIndex);
            list.Add(menuPageTypes);
            list.Add(menuBlockTypes);
            list.Add(menuVisitorGroups);
            return list;
        }
    }
}