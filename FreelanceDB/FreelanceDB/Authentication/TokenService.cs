using FreelanceDB.Authentication.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FreelanceDB.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly string Issuer;
        private readonly string Audience;
        private readonly string Key;
        private readonly int RefressExpiryDays=2;
        public TokenService(IConfiguration config)
        {
            Issuer = config["Token:Issuer"];
            Audience = config["Token:Audience"];
            Key = config["Token:Key"];
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
                Issuer = this.Issuer,
                Audience = this.Audience,
                Expires = DateTime.Now.AddMinutes(AuthOptions.AccessTokenExpirationTime),
                SigningCredentials = new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(Key),
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
    

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)//метод позволяет проверить и извлечь информацию из JWT-токена, даже если он истек, при условии, что токен был правильно подписан известным ключом подписи и содержит valid идентификационные данные
        {
            var parameters = new TokenValidationParameters
            {
                ValidIssuer = this.Issuer,
                ValidAudience = this.Audience,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.GetSymmetricSecurityKey(Key).ToString())),
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
    }
}
