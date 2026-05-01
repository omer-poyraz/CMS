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
        IMailTemplateService MailTemplateService { get; }
        IMenuService MenuService { get; }
        IMenuGroupService MenuGroupService { get; }
        IPageService PageService { get; }
        IPopupService PopupService { get; }
        ISettingsService SettingsService { get; }
        IUserService UserService { get; }
    }
}
