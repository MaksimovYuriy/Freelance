using FreelanceDB.Models;
using System.Security.Claims;

namespace FreelanceDB.Authentication
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserModel user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token)

    }
}
