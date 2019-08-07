using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace jwtcore.Autenticacao
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            // using (var provider = new RSACryptoServiceProvider(2048))
            // {
            //     Key = new RsaSecurityKey(provider.ExportParameters(true));
            // }
            // SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

            var keyString = "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs";
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }

        private string GenerateToken1()
        {
            // https://stackoverflow.com/questions/46202176/jwt-generation-and-validation-in-net-throws-key-is-not-supported

            var tokenHandler = new JwtSecurityTokenHandler();
            var certificate = new X509Certificate2(@"C:\Users\myname\my-cert.pfx", "mypassword");
            var securityKey = new X509SecurityKey(certificate);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Issuer = "Self",
                IssuedAt = DateTime.Now,
                Audience = "Others",
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.RsaSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool ValidateToken1(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var certificate = new X509Certificate2(@"C:\Users\myname\my-cert.pfx", "mypassword");
            var securityKey = new X509SecurityKey(certificate);

            var validationParameters = new TokenValidationParameters
            {
                ValidAudience = "Others",
                ValidIssuer = "Self",
                IssuerSigningKey = securityKey
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
            if (principal == null)
                return false;
            if (securityToken == null)
                return false;

            return true;
        }
    }
}