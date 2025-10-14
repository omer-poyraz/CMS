namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IBasketRepository BasketRepository { get; }
        ICommentRepository CommentRepository { get; }
        IContentRepository ContentRepository { get; }
        IFilesRepository FilesRepository { get; }
        IGoogleAnalyticsRepository GoogleAnalyticsRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        ILogRepository LogRepository { get; }
        IMenuRepository MenuRepository { get; }
        IMenuGroupRepository MenuGroupRepository { get; }
        IPageRepository PageRepository { get; }
        IPopupRepository PopupRepository { get; }
        IProductRepository ProductRepository { get; }
        ISettingsRepository SettingsRepository { get; }
        IUnitRepository UnitRepository { get; }
        IUserRepository UserRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IVersioningRepository VersioningRepository { get; }
        IVideoGroupRepository VideoGroupRepository { get; }
        IVideoRepository VideoRepository { get; }
        Task SaveAsync();
    }
}
