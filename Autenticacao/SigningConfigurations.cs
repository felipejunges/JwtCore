using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace jwtcore.Autenticacao
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            //var size = Encoding.UTF8.GetString(Key);
            //var outroSize = size;

            //Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MTIzNDU2Nzk4MDEyMzQ1Njc5ODAxMjM0NTY3OTgwMTIzNDU2Nzk4MDEyMzQ1Njc5ODAxMjM0NTY3OTgwMTIzNA=="));
            //Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyJhZG1pbl9hcGlwcm9kdXRvcyIsImFkbWluX2FwaXByb2R1dG9zIl0sImp0aSI6ImI3N2U3ZTI2YTVkNjRkODY5YTAxYzBkODFiZDljNDRiIiwibmJmIjoxNTQ2ODE3MzY4LCJleHAiOjE1NDY4MTg1NjgsImlhdCI6MTU0NjgxNzM2OCwiaXNzIjoiRXhlbXBsb0lzc3VlciIsImF1ZCI6IkV4ZW1wbG9BdWRpZW5jZSJ9.pYh_-pNvPThUbDmS9Q5cfaGHrrlN-EAb8Jz8n4dhF6ddA40_kj8EuVedy_z4Q2ADMLews7mbqij7WGYsmZtfOhA2x2uxkL5w_ChOsU0_WcKlLmBed7QY4lUukizNutNlzzpIzeQe1LGqAlq5Y2xCVScfv5oghzGZ8cgXNwk7Z8PY9Ykjp0ipUBO1ara1nwGOwFzx0KVKCzj7sCoHe6pX43IQOdlviJ57EGQfu96vwvgozzS8Ppgu0rsOHkQu_fnWCfABpTX_zznUXsMLuRYDft1OWjb1zE-cq2l1APCor7vrWDRliCh-nQ-j09AGqE_7UZCNRBhLS48384sWmmx7Sw"));

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}