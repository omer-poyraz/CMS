namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ICommentRepository CommentRepository { get; }
        IFilesRepository FilesRepository { get; }
        IGoogleAnalyticsRepository GoogleAnalyticsRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        ILogRepository LogRepository { get; }
        IMenuRepository MenuRepository { get; }
        IMenuGroupRepository MenuGroupRepository { get; }
        IPageRepository PageRepository { get; }
        IPopupRepository PopupRepository { get; }
        ISettingsRepository SettingsRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
