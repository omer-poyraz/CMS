using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IBasketRepository _basketRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly IGoogleAnalyticsRepository _googleAnalyticsRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogRepository _logRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IPopupRepository _popupRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IVersioningRepository _versioningRepository;
        private readonly IVideoGroupRepository _videoGroupRepository;
        private readonly IVideoRepository _videoRepository;

        public RepositoryManager(
            RepositoryContext context,
            IBasketRepository basketRepository,
            ICommentRepository commentRepository,
            IContentRepository contentRepository,
            IFilesRepository filesRepository,
            IGoogleAnalyticsRepository googleAnalyticsRepository,
            IMenuRepository menuRepository,
            IMenuGroupRepository menuGroupRepository,
            ILanguageRepository languageRepository,
            ILogRepository logRepository,
            IPageRepository pageRepository,
            IPopupRepository popupRepository,
            IProductRepository productRepository,
            ISettingsRepository settingsRepository,
            IUnitRepository unitRepository,
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository,
            IVersioningRepository versioningRepository,
            IVideoGroupRepository videoGroupRepository,
            IVideoRepository videoRepository)
        {
            _context = context;
            _basketRepository = basketRepository;
            _commentRepository = commentRepository;
            _contentRepository = contentRepository;
            _filesRepository = filesRepository;
            _googleAnalyticsRepository = googleAnalyticsRepository;
            _menuRepository = menuRepository;
            _menuGroupRepository = menuGroupRepository;
            _languageRepository = languageRepository;
            _logRepository = logRepository;
            _pageRepository = pageRepository;
            _popupRepository = popupRepository;
            _productRepository = productRepository;
            _settingsRepository = settingsRepository;
            _unitRepository = unitRepository;
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _versioningRepository = versioningRepository;
            _videoGroupRepository = videoGroupRepository;
            _videoRepository = videoRepository;
        }

        public IBasketRepository BasketRepository => _basketRepository;
        public ICommentRepository CommentRepository => _commentRepository;
        public IContentRepository ContentRepository => _contentRepository;
        public IFilesRepository FilesRepository => _filesRepository;
        public IGoogleAnalyticsRepository GoogleAnalyticsRepository => _googleAnalyticsRepository;
        public IMenuRepository MenuRepository => _menuRepository;
        public IMenuGroupRepository MenuGroupRepository => _menuGroupRepository;
        public ILanguageRepository LanguageRepository => _languageRepository;
        public ILogRepository LogRepository => _logRepository;
        public IPageRepository PageRepository => _pageRepository;
        public IPopupRepository PopupRepository => _popupRepository;
        public IProductRepository ProductRepository => _productRepository;
        public ISettingsRepository SettingsRepository => _settingsRepository;
        public IUnitRepository UnitRepository => _unitRepository;
        public IUserRepository UserRepository => _userRepository;
        public IUserProfileRepository UserProfileRepository => _userProfileRepository;
        public IVersioningRepository VersioningRepository => _versioningRepository;
        public IVideoGroupRepository VideoGroupRepository => _videoGroupRepository;
        public IVideoRepository VideoRepository => _videoRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
