using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IBasketService _basketService;
        private readonly ICommentService _commentService;
        private readonly IContentService _contentService;
        private readonly IFilesService _filesService;
        private readonly IGoogleAnalyticsService _googleAnalyticsService;
        private readonly ILanguageService _languageService;
        private readonly ILogService _logService;
        private readonly IMenuService _menuService;
        private readonly IMenuGroupService _menuGroupService;
        private readonly IPageService _pageService;
        private readonly IPopupService _popupService;
        private readonly IProductService _productService;
        private readonly ISettingsService _settingsService;
        private readonly IUnitService _unitService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IVersioningService _versioningService;
        private readonly IVideoGroupService _videoGroupService;
        private readonly IVideoService _videoService;

        public ServiceManager(
            IAuthenticationService authenticationService,
            IBasketService basketService,
            ICommentService commentService,
            IContentService contentService,
            IFilesService filesService,
            IGoogleAnalyticsService googleAnalyticsService,
            ILanguageService languageService,
            ILogService logService,
            IMenuService menuService,
            IMenuGroupService menuGroupService,
            IPageService pageService,
            IPopupService popupService,
            IProductService productService,
            ISettingsService settingsService,
            IUnitService unitService,
            IUserService userService,
            IUserProfileService userProfileService,
            IVersioningService versioningService,
            IVideoGroupService videoGroupService,
            IVideoService videoService)
        {
            _authenticationService = authenticationService;
            _basketService = basketService;
            _commentService = commentService;
            _contentService = contentService;
            _filesService = filesService;
            _googleAnalyticsService = googleAnalyticsService;
            _languageService = languageService;
            _logService = logService;
            _menuService = menuService;
            _menuGroupService = menuGroupService;
            _pageService = pageService;
            _popupService = popupService;
            _productService = productService;
            _settingsService = settingsService;
            _unitService = unitService;
            _userService = userService;
            _userProfileService = userProfileService;
            _versioningService = versioningService;
            _videoGroupService = videoGroupService;
            _videoService = videoService;
        }

        public IAuthenticationService AuthenticationService => _authenticationService;
        public IBasketService BasketService => _basketService;
        public ICommentService CommentService => _commentService;
        public IContentService ContentService => _contentService;
        public IFilesService FilesService => _filesService;
        public IGoogleAnalyticsService GoogleAnalyticsService => _googleAnalyticsService;
        public ILanguageService LanguageService => _languageService;
        public ILogService LogService => _logService;
        public IMenuService MenuService => _menuService;
        public IMenuGroupService MenuGroupService => _menuGroupService;
        public IPageService PageService => _pageService;
        public IPopupService PopupService => _popupService;
        public IProductService ProductService => _productService;
        public ISettingsService SettingsService => _settingsService;
        public IUnitService UnitService => _unitService;
        public IUserService UserService => _userService;
        public IUserProfileService UserProfileService => _userProfileService;
        public IVersioningService VersioningService => _versioningService;
        public IVideoGroupService VideoGroupService => _videoGroupService;
        public IVideoService VideoService => _videoService;
    }
}
