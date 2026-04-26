using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICommentService _commentService;
        private readonly IFilesService _filesService;
        private readonly IGoogleAnalyticsService _googleAnalyticsService;
        private readonly ILanguageService _languageService;
        private readonly ILogService _logService;
        private readonly IMenuService _menuService;
        private readonly IMenuGroupService _menuGroupService;
        private readonly IPageService _pageService;
        private readonly IPageSectionService _pageSectionService;
        private readonly IPageTranslationService _pageTranslationService;
        private readonly IPopupService _popupService;
        private readonly ISectionFieldService _sectionFieldService;
        private readonly ISectionItemService _sectionItemService;
        private readonly ISettingsService _settingsService;
        private readonly IUserService _userService;

        public ServiceManager(
            IAuthenticationService authenticationService,
            ICommentService commentService,
            IFilesService filesService,
            IGoogleAnalyticsService googleAnalyticsService,
            ILanguageService languageService,
            ILogService logService,
            IMenuService menuService,
            IMenuGroupService menuGroupService,
            IPageService pageService,
            IPageSectionService pageSectionService,
            IPageTranslationService pageTranslationService,
            IPopupService popupService,
            ISectionFieldService sectionFieldService,
            ISectionItemService sectionItemService,
            ISettingsService settingsService,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _commentService = commentService;
            _filesService = filesService;
            _googleAnalyticsService = googleAnalyticsService;
            _languageService = languageService;
            _logService = logService;
            _menuService = menuService;
            _menuGroupService = menuGroupService;
            _pageService = pageService;
            _pageSectionService = pageSectionService;
            _pageTranslationService = pageTranslationService;
            _popupService = popupService;
            _sectionFieldService = sectionFieldService;
            _sectionItemService = sectionItemService;
            _settingsService = settingsService;
            _userService = userService;
        }

        public IAuthenticationService AuthenticationService => _authenticationService;
        public ICommentService CommentService => _commentService;
        public IFilesService FilesService => _filesService;
        public IGoogleAnalyticsService GoogleAnalyticsService => _googleAnalyticsService;
        public ILanguageService LanguageService => _languageService;
        public ILogService LogService => _logService;
        public IMenuService MenuService => _menuService;
        public IMenuGroupService MenuGroupService => _menuGroupService;
        public IPageService PageService => _pageService;
        public IPageSectionService PageSectionService => _pageSectionService;
        public IPageTranslationService PageTranslationService => _pageTranslationService;
        public IPopupService PopupService => _popupService;
        public ISectionFieldService SectionFieldService => _sectionFieldService;
        public ISectionItemService SectionItemService => _sectionItemService;
        public ISettingsService SettingsService => _settingsService;
        public IUserService UserService => _userService;
    }
}
