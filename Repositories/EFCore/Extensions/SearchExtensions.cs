using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore.Extensions
{
    public static class SearchExtensions
    {
        public static IQueryable<User> SearchUser(this IQueryable<User> user, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return user;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return user.Where(u => u.UserName!.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Log> SearchLog(this IQueryable<Log> log, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return log;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return log.Where(l =>
                (l.ServiceName != null && l.ServiceName.ToLower().Contains(lowerCaseTerm)) ||
                (l.Message != null && l.Message.ToLower().Contains(lowerCaseTerm)) ||
                (l.Process != null && l.Process.ToLower().Contains(lowerCaseTerm)) ||
                (l.Result != null && l.Result.ToLower().Contains(lowerCaseTerm)) ||
                (l.Ip != null && l.Ip.ToLower().Contains(lowerCaseTerm)) ||
                (l.UserId != null && l.UserId.ToLower().Contains(lowerCaseTerm)) ||
                (l.User != null && l.User.UserName != null && l.User.UserName.ToLower().Contains(lowerCaseTerm))
            );
        }

        public static IQueryable<Files> SearchFile(this IQueryable<Files> files, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return files;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return files.Where(f =>
                (f.FileUrl != null && f.FileUrl.ToLower().Contains(lowerCaseTerm)) ||
                (f.FileType != null && f.FileType.ToLower().Contains(lowerCaseTerm))
            );
        }
    }
}
