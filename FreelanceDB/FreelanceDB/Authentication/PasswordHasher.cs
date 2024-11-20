using FreelanceDB.Authentication.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace FreelanceDB.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            throw new NotImplementedException();
        }
    }
}
