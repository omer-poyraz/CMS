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
        private readonly IPageSectionRepository _pageSectionRepository;
        private readonly IPageTranslationRepository _pageTranslationRepository;
        private readonly IPopupRepository _popupRepository;
        private readonly ISectionFieldRepository _sectionFieldRepository;
        private readonly ISectionItemRepository _sectionItemRepository;
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
            IPageSectionRepository pageSectionRepository,
            IPageTranslationRepository pageTranslationRepository,
            IPopupRepository popupRepository,
            ISectionFieldRepository sectionFieldRepository,
            ISectionItemRepository sectionItemRepository,
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
            _pageSectionRepository = pageSectionRepository;
            _pageTranslationRepository = pageTranslationRepository;
            _popupRepository = popupRepository;
            _sectionFieldRepository = sectionFieldRepository;
            _sectionItemRepository = sectionItemRepository;
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
        public IPageSectionRepository PageSectionRepository => _pageSectionRepository;
        public IPageTranslationRepository PageTranslationRepository => _pageTranslationRepository;
        public IPopupRepository PopupRepository => _popupRepository;
        public ISectionFieldRepository SectionFieldRepository => _sectionFieldRepository;
        public ISectionItemRepository SectionItemRepository => _sectionItemRepository;
        public ISettingsRepository SettingsRepository => _settingsRepository;
        public IUserRepository UserRepository => _userRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
