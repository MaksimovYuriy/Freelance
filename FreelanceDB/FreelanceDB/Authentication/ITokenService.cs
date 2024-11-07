using FreelanceDB.Models;

namespace FreelanceDB.Authentication
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserModel user);
        string GenerateRefreshToken();
    }
}
