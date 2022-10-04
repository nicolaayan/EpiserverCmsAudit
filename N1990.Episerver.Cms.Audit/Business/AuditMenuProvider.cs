using System.Collections.Generic;
using EPiServer.Framework.Localization;
using EPiServer.Shell;
using EPiServer.Shell.Navigation;

namespace N1990.Episerver.Cms.Audit.Business
{
    [MenuProvider]
    public class AuditMenuProvider : IMenuProvider
    {
        public const string MenuPath = "/global/cms/audit/";
        private readonly LocalizationService _localizationService;

        public AuditMenuProvider(LocalizationService localizationService)
        {
            this._localizationService = localizationService;
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            var sectionMenu =
                new UrlMenuItem(
                        _localizationService.GetString("/cmsaudit/menus/audit", "Audit"),
                        MenuPath,
                        Paths.ToResource("CmsAudit", "CmsAudit"))
                {
                    IsAvailable = _ => true,
                    SortIndex = int.MaxValue
                };

            var menuSites =
                new UrlMenuItem(
                    _localizationService.GetString("/cmsaudit/menus/audit/sites", "Sites"),
                    MenuPath + "sites",
                    Paths.ToResource("CmsAudit", "CmsAudit"))
                {
                    IsAvailable = _ => true,
                    SortIndex = 100
                };

            var menuPageTypes =
                new UrlMenuItem(
                    _localizationService.GetString("/cmsaudit/menus/audit/pagetypes", "Page Types"),
                    MenuPath + "pagetypes",
                    Paths.ToResource("CmsAudit", "PageTypes"))
                {
                    IsAvailable = _ => true,
                    SortIndex = 200
                };

            var menuBlockTypes =
                new UrlMenuItem(
                    _localizationService.GetString("/cmsaudit/menus/audit/blocktypes", "Block Types"),
                    MenuPath + "blocktypes",
                    Paths.ToResource("CmsAudit", "BlockTypes"))
                {
                    IsAvailable = _ => true,
                    SortIndex = 300
                };

            var menuVisitorGroups =
                new UrlMenuItem(
                    _localizationService.GetString("/cmsaudit/menus/audit/visitorgroups", "Visitor Groups"),
                    MenuPath + "visitorgroups",
                    Paths.ToResource("CmsAudit", "VisitorGroups"))
                {
                    IsAvailable = _ => true,
                    SortIndex = 400
                };

            var list = new List<MenuItem>
            {
                sectionMenu,
                menuSites,
                menuPageTypes,
                menuBlockTypes,
                menuVisitorGroups
            };
            return list;
        }
    }
}