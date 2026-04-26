namespace Services.Contracts
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        ICommentService CommentService { get; }
        IFilesService FilesService { get; }
        IGoogleAnalyticsService GoogleAnalyticsService { get; }
        ILanguageService LanguageService { get; }
        ILogService LogService { get; }
        IMenuService MenuService { get; }
        IMenuGroupService MenuGroupService { get; }
        IPageService PageService { get; }
        IPageSectionService PageSectionService { get; }
        IPageTranslationService PageTranslationService { get; }
        IPopupService PopupService { get; }
        ISectionFieldService SectionFieldService { get; }
        ISectionItemService SectionItemService { get; }
        ISettingsService SettingsService { get; }
        IUserService UserService { get; }
    }
}
