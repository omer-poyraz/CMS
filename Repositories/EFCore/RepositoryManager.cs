using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ICommentRepository _commentRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly IGoogleAnalyticsRepository _googleAnalyticsRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogRepository _logRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IPopupRepository _popupRepository;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IUserRepository _userRepository;

        public RepositoryManager(
            RepositoryContext context,
            ICommentRepository commentRepository,
            IFilesRepository filesRepository,
            IGoogleAnalyticsRepository googleAnalyticsRepository,
            IMenuRepository menuRepository,
            IMenuGroupRepository menuGroupRepository,
            ILanguageRepository languageRepository,
            ILogRepository logRepository,
            IPageRepository pageRepository,
            IPopupRepository popupRepository,
            ISettingsRepository settingsRepository,
            IUserRepository userRepository)
        {
            _context = context;
            _commentRepository = commentRepository;
            _filesRepository = filesRepository;
            _googleAnalyticsRepository = googleAnalyticsRepository;
            _menuRepository = menuRepository;
            _menuGroupRepository = menuGroupRepository;
            _languageRepository = languageRepository;
            _logRepository = logRepository;
            _pageRepository = pageRepository;
            _popupRepository = popupRepository;
            _settingsRepository = settingsRepository;
            _userRepository = userRepository;
        }

        public ICommentRepository CommentRepository => _commentRepository;
        public IFilesRepository FilesRepository => _filesRepository;
        public IGoogleAnalyticsRepository GoogleAnalyticsRepository => _googleAnalyticsRepository;
        public IMenuRepository MenuRepository => _menuRepository;
        public IMenuGroupRepository MenuGroupRepository => _menuGroupRepository;
        public ILanguageRepository LanguageRepository => _languageRepository;
        public ILogRepository LogRepository => _logRepository;
        public IPageRepository PageRepository => _pageRepository;
        public IPopupRepository PopupRepository => _popupRepository;
        public ISettingsRepository SettingsRepository => _settingsRepository;
        public IUserRepository UserRepository => _userRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
