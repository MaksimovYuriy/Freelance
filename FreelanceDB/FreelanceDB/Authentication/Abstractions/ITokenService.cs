using FreelanceDB.Database.Entities;
using System.Security.Claims;

namespace FreelanceDB.Authentication.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(long id, string role);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        DateTime GetRefreshTokenExpireTime();
        Task<string> RefreshAccessToken(long userId);
    }
}
