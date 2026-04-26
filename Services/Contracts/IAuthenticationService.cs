using Entities.DTOs.UserDto;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterRequest(UserForRegisterDto registerDto);
        Task<IdentityResult> RegisterUser(UserForRegisterDto registerDto);
        Task<IdentityResult> RegisterConfirm(UserForRegisterDto registerDto, string code);
        Task<bool> ValidUser(UserForAuthenticationDto authenticationDto);
        Task<TokenDto> CreateToken(bool populateExpire);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
