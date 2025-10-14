namespace Services.Contracts
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        IBasketService BasketService { get; }
        ICommentService CommentService { get; }
        IContentService ContentService { get; }
        IFilesService FilesService { get; }
        IGoogleAnalyticsService GoogleAnalyticsService { get; }
        ILanguageService LanguageService { get; }
        ILogService LogService { get; }
        IMenuService MenuService { get; }
        IMenuGroupService MenuGroupService { get; }
        IPageService PageService { get; }
        IPopupService PopupService { get; }
        IProductService ProductService { get; }
        ISettingsService SettingsService { get; }
        IUnitService UnitService { get; }
        IUserService UserService { get; }
        IUserProfileService UserProfileService { get; }
        IVersioningService VersioningService { get; }
        IVideoGroupService VideoGroupService { get; }
        IVideoService VideoService { get; }
    }
}
