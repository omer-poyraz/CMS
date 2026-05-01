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
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMenuService _menuService;
        private readonly IMenuGroupService _menuGroupService;
        private readonly IPageService _pageService;
        private readonly IPopupService _popupService;
        private readonly ISettingsService _settingsService;
        private readonly IUserService _userService;

        public ServiceManager(
            IAuthenticationService authenticationService,
            ICommentService commentService,
            IFilesService filesService,
            IGoogleAnalyticsService googleAnalyticsService,
            ILanguageService languageService,
            ILogService logService,
            IMailTemplateService mailTemplateService,
            IMenuService menuService,
            IMenuGroupService menuGroupService,
            IPageService pageService,
            IPopupService popupService,
            ISettingsService settingsService,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _commentService = commentService;
            _filesService = filesService;
            _googleAnalyticsService = googleAnalyticsService;
            _languageService = languageService;
            _logService = logService;
            _mailTemplateService = mailTemplateService;
            _menuService = menuService;
            _menuGroupService = menuGroupService;
            _pageService = pageService;
            _popupService = popupService;
            _settingsService = settingsService;
            _userService = userService;
        }

        public IAuthenticationService AuthenticationService => _authenticationService;
        public ICommentService CommentService => _commentService;
        public IFilesService FilesService => _filesService;
        public IGoogleAnalyticsService GoogleAnalyticsService => _googleAnalyticsService;
        public ILanguageService LanguageService => _languageService;
        public ILogService LogService => _logService;
        public IMailTemplateService MailTemplateService => _mailTemplateService;
        public IMenuService MenuService => _menuService;
        public IMenuGroupService MenuGroupService => _menuGroupService;
        public IPageService PageService => _pageService;
        public IPopupService PopupService => _popupService;
        public ISettingsService SettingsService => _settingsService;
        public IUserService UserService => _userService;
    }
}
