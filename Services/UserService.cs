using AutoMapper;
using Entities.DTOs.UserDto;
using Entities.RequestFeature;
using Entities.RequestFeature.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly UserManager<Entities.Models.User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public UserService(IRepositoryManager manager, IMapper mapper, UserManager<Entities.Models.User> userManager, IConfiguration configuration, IMailService mailService)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<UserDto> DeleteOneUserAsync(string? userId, bool? trackChanges)
        {
            var user = await _manager.UserRepository.GetOneUserByIdAsync(userId, trackChanges);
            _manager.UserRepository.DeleteOneUser(user);
            await _manager.SaveAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<(IEnumerable<UserDto> userDtos, MetaData metaData)> GetAllUsersAsync(UserParameters userParameters, bool? trackChanges)
        {
            var users = await _manager.UserRepository.GetAllUsersAsync(userParameters, trackChanges);
            var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return (userDto, users.MetaData);
        }

        public async Task<UserDto> GetOneUserByIdAsync(string? userId, bool? trackChanges)
        {
            var user = await _manager.UserRepository.GetOneUserByIdAsync(userId, trackChanges);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateOneUserAsync(string? userId, UserDtoForUpdate userDtoForUpdate, bool? trackChanges)
        {
            var userDto = await _manager.UserRepository.GetOneUserByIdAsync(userId, trackChanges);
            var user = await _userManager.FindByEmailAsync(userDto.Email!);

            user!.UserName = userDtoForUpdate.UserName;
            user.FirstName = userDtoForUpdate.FirstName;
            user.LastName = userDtoForUpdate.LastName;
            user.Email = userDtoForUpdate.Email;
            user.PhoneNumber = userDtoForUpdate.PhoneNumber;
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> ChangePasswordAsync(string? userId, string currentPassword, string newPassword, bool? trackChanges)
        {
            var userDto = await _manager.UserRepository.GetOneUserByIdAsync(userId, trackChanges);
            var user = await _userManager.FindByEmailAsync(userDto.Email!);
            await _userManager.ChangePasswordAsync(user!, currentPassword, newPassword);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> ResetPasswordAsync(string? mail)
        {
            if (string.IsNullOrEmpty(mail))
                throw new InvalidOperationException("E-posta adresi boş olamaz.");

            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null)
                throw new InvalidOperationException("Bu e-posta adresi sistemimizde kayıtlı değildir.");

            try
            {
                var EmailInfo = _configuration.GetSection("EmailInfo");
                var deepLink = _configuration.GetSection("EmailInfo")["ResetPasswordLink"]!.ToString()!.Replace("_email_", mail);

                string subject = "Şifre Sıfırlama Talebi";
                string body = $@"
                            <!DOCTYPE html>
                            <html>
                                <head>
                                    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
                                </head>
                                <body style=""margin: 0; padding: 20px; font-family: Arial, sans-serif;"">
                                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                        <tr>
                                            <td align=""center"">
                                                <h2>Şifre Sıfırlama</h2>
                                                <p>Merhaba,</p>
                                                <p>North Soft hesabınız için şifre sıfırlama talebinde bulundunuz.</p>
                                                <p>Eğer bu talepte siz bulunduysanız lütfen aşağıdaki bağlantıya tıklayarak şifrenizi sıfırlayabilirsiniz.</p>
                                                <!-- Ana Link Butonu -->
                                                <div onclick=""window.location.href='{deepLink}'"" 
                                                   style=""display: inline-block; 
                                                          background-color: #007bff; 
                                                          color: #ffffff;
                                                          padding: 12px 24px;
                                                          text-decoration: none;
                                                          border-radius: 4px;
                                                          margin: 20px 0;
                                                          font-weight: bold;"">
                                                    Şifremi Sıfırla
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </body>
                            </html>";
                await _mailService.SendMailAsync(mail!, subject, body, true);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Şifre sıfırlama e-postası gönderilirken bir hata oluştu.", ex);
            }
        }

        public async Task<(IEnumerable<UserDto> userDtos, MetaData metaData)> GetAllUnActiveUsersAsync(UserParameters userParameters, bool? trackChanges)
        {
            var users = await _manager.UserRepository.GetAllUnActiveUsersAsync(userParameters, trackChanges);
            var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return (userDto, users.MetaData);
        }

        public async Task<UserDto> UserActivationAsync(string userId, bool trackChanges)
        {
            var userDto = await _manager.UserRepository.GetOneUserByIdAsync(userId, trackChanges);
            var user = await _userManager.FindByEmailAsync(userDto.Email!);
            var name = user!.FirstName + " " + user.LastName;

            user!.Active = true;
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            await SendWelcomeMailAsync(user.Email!, name);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UserIsMealAsync(string userId, bool isMeal, bool trackChanges)
        {
            var userDto = await _manager.UserRepository.GetOneUserByIdAsync(userId, trackChanges);
            var user = await _userManager.FindByEmailAsync(userDto.Email!);
            var name = user!.FirstName + " " + user.LastName;

            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            await SendWelcomeMailAsync(user.Email!, name);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UserUnActiveAsync(string userId, bool trackChanges)
        {
            var userDto = await _manager.UserRepository.GetOneUserByIdAsync(userId, trackChanges);
            var user = await _userManager.FindByEmailAsync(userDto.Email!);
            var name = user!.FirstName + " " + user.LastName;

            user!.Active = false;
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            await SendUnActiveMailAsync(user.Email!, name);

            return _mapper.Map<UserDto>(user);
        }

        public async Task SendWelcomeMailAsync(string to, string name)
        {
            string subject = "Hoşgeldiniz!";
            string body = $"Merhaba {name},<br><br>Kaydınız başarıyla oluşturuldu. Lütfen hesabınıza giriş yapınız.";
            await _mailService.SendMailAsync(to, subject, body);
        }

        public async Task SendUnActiveMailAsync(string to, string name)
        {
            string subject = "Hesabınız Kapatıldı";
            string body = $"Merhaba {name},<br><br>Kaydınız yetkililer tarafından pasife alınmıştır. İyi Günler dileriz.";
            await _mailService.SendMailAsync(to, subject, body);
        }
    }
}
