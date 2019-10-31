using JwtCore.Configuration;
using JwtCore.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace JwtCore.Services
{
    public class JwtService
    {
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public JwtService(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public JwtToken CriarToken(UsuarioEntity usuario)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.Login),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UsuarioId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, "Felipe Junges"),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Role, "Gerente")
                    }
                );

            var dataCriacao = DateTime.Now;
            var dataExpiracao = dataCriacao + TimeSpan.FromMinutes(_tokenConfigurations.Minutes);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            var refreshToken = GenerateRefreshToken();

            return JwtToken.Criar(token, refreshToken, dataCriacao, dataExpiracao);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingConfigurations.Key,
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}