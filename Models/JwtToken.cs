using System;

namespace JwtCore.Models
{
    public class JwtToken
    {
        public JwtToken(string token, string refreshToken, DateTime create, DateTime expiration)
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

        public static JwtToken Criar(string token, string refreshToken, DateTime create, DateTime expiration)
        {
            return new JwtToken(token, refreshToken, create, expiration);
        }
    }
}