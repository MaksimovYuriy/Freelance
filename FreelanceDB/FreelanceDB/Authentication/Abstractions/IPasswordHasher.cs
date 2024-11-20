﻿namespace FreelanceDB.Authentication.Abstractions
{
    public interface IPasswordHasher
    {
        public bool VerifyPassword(string password, string hash, byte[] salt);
        public string HashPassword(string password);
    }
}
