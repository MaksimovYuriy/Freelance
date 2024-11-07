namespace FreelanceDB.Models
{
    public class UserModel//TODO: роли, бан
    {
        public long Id { get; set; }

        public string Login { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Nickname { get; set; } = null!;

        public string Role { get; set; }

        public string? AToken { get; set; }

        public string? RToken { get; set; }

        public int Balance { get; set; }

        public int FreezeBalance { get; set; }

        public UserModel( long id, string login, string passwordHash, string nickname, string? aToken, string? rToken, int balance, int freezeBalance)
        {
            Id = id;
            Login = login;
            PasswordHash = passwordHash;
            Nickname = nickname;
            AToken = aToken;
            RToken = rToken;
            Balance = balance;
            FreezeBalance = freezeBalance;
        }
        internal UserModel() { }
    }
}
