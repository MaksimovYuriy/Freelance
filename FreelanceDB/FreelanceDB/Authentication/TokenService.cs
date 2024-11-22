using FreelanceDB.Abstractions.Services;
using FreelanceDB.Authentication.Abstractions;
using FreelanceDB.Database.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FreelanceDB.Authentication
{
    public class TokenService : ITokenService
    {
        const int RefressExpiryDays = 2;
        private readonly IUserService _userService;

        public TokenService(IUserService userService)
        {
            _userService = userService;
        }
        public string GenerateAccessToken(long id, string role)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Role,role)
                }),
                Issuer = AuthOptions.Issuer,
                Audience = AuthOptions.Audience,
                Expires = DateTime.Now.AddMinutes(AuthOptions.AccessTokenExpirationTime),
                SigningCredentials = new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.GetSymmetricSecurityKey().ToString())),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public DateTime GetRefreshTokenExpireTime()
        {
            return DateTime.UtcNow.AddDays(RefressExpiryDays);
        }

        public async Task<string> RefreshAccessToken(long userId)
        {
            (string, DateTime) data = await _userService.GetRTokenAndExpiryTime(userId);
            if (data.Item2< DateTime.UtcNow)
            {
                await _userService.RemoveTokens(userId);
                return "";
            }
            else
            {
                var user = await _userService.GetUser(userId);
                var atoken = GenerateAccessToken(userId, user.RoleId.ToString());
                user.AToken = atoken;
                await  _userService.UpdateUser(user);
                return atoken;
            }
        }
    }
}
