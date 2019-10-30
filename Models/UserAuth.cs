using System;

namespace JwtCore.Models
{
    public class UserAuth
    {
        public UserAuth(string token, string refreshToken, DateTime create, DateTime expiration)
        {
            Token = token;
            RefreshToken = refreshToken;
            Create = create;
            Expiration = expiration;
        }

        public string Token { get; private set; }

        public string RefreshToken { get; private set; }

        public DateTime Create { get; private set; }

        public DateTime Expiration { get; private set; }

        public static UserAuth Criar(string token, string refreshToken, DateTime create, DateTime expiration)
        {
            return new UserAuth(token, refreshToken, create, expiration);
        }
    }
}