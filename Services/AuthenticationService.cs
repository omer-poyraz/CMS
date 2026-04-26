using Microsoft.Extensions.Caching.Memory;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Entities.DTOs.UserDto;
using Entities.Exceptions.User;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Contracts;
using Repositories.EFCore;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMailService _mailService;
        private readonly IMemoryCache _cache;
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _manager;
        private User? _user;

        public AuthenticationService(
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration,
            RepositoryContext context,
            IRepositoryManager manager,
            IMailService mailService,
            IMemoryCache cache)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _manager = manager;
            _mailService = mailService;
            _cache = cache;
        }

        public async Task<TokenDto> CreateToken(bool populateExpire)
        {
            if (_user == null)
            {
                throw new InvalidOperationException("User is not set.");
            }

            var signingCredentials = GetSigninCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;
            _user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var role = await _userManager.GetRolesAsync(_user);
            var isAdmin = role.Contains("Admin") || role.Contains("Super Admin") ? 1 : 0;

            return new TokenDto
            {
                IsAdmin = isAdmin,
                User = _user,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

            if (
                user == null
                || user.RefreshToken != tokenDto.RefreshToken
                || user.RefreshTokenExpireTime <= DateTime.UtcNow
            )
                throw new RefreshTokenBadRequestException();

            _user = user;

            return await CreateToken(false);
        }

        public async Task<bool> RegisterRequest(UserForRegisterDto registerDto)
        {
            var code = new Random().Next(100000, 999999).ToString();
            _cache.Set($"register:{registerDto.Email}", code, TimeSpan.FromMinutes(3));
            string subject = "Kayıt Doğrulama Kodu";
            string body = $"Kayıt işlemini tamamlamak için doğrulama kodunuz: <b>{code}</b> (3 dakika geçerlidir)";
            await _mailService.SendMailAsync(registerDto.Email!, subject, body);
            return true;
        }

        public async Task<IdentityResult> RegisterConfirm(UserForRegisterDto registerDto, string code)
        {
            var cacheKey = $"register:{registerDto.Email}";
            if (!_cache.TryGetValue(cacheKey, out string? cachedCode) || cachedCode != code)
            {
                _cache.Remove(cacheKey);
                await RegisterRequest(registerDto);
                throw new InvalidOperationException("Doğrulama kodunuz doğru değil. Yeni bir kod gönderildi.");
            }
            _cache.Remove(cacheKey);
            var user = _mapper.Map<User>(registerDto);
            user.Active = false;
            var result = await _userManager.CreateAsync(user, registerDto.Password!);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, registerDto.Roles!);
            return result;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email!);
            if (existingUser != null)
                throw new InvalidOperationException("Bu e-posta ile zaten bir kullanıcı mevcut.");

            var userNameToCheck = registerDto.UserName?.Trim();
            var userNameExists = !string.IsNullOrEmpty(userNameToCheck) && await _userManager.FindByNameAsync(userNameToCheck) != null;
            string finalUserName = userNameToCheck ?? string.Empty;
            if (userNameExists)
            {
                var random = new Random();
                int randomNumber = random.Next(1000, 9999);
                finalUserName = $"{finalUserName}-{randomNumber}";
            }

            var user = _mapper.Map<User>(registerDto);
            user.UserName = finalUserName;
            user.Active = true;
            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, registerDto.Password!);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, registerDto.Roles!);
            }
            return result;
        }

        public async Task<bool> ValidUser(UserForAuthenticationDto authenticationDto)
        {
            if (!string.IsNullOrWhiteSpace(authenticationDto.UserName) && authenticationDto.UserName.Contains("@"))
            {
                _user = await _userManager.FindByEmailAsync(authenticationDto.UserName!);
            }
            else
            {
                _user = await _userManager.FindByNameAsync(authenticationDto.UserName!);
            }
            var result = (
                _user != null
                && _user.Active == true
                && await _userManager.CheckPasswordAsync(_user!, authenticationDto.Password!)
            );
            return result;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string? accessToken)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(
                accessToken,
                tokenValidationParameters,
                out securityToken
            );

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (
                jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
            {
                throw new SecurityTokenException("Invalid Token!");
            }

            return principal;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private SigningCredentials GetSigninCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, _user!.UserName!) };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
