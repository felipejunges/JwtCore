using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace jwtcore.Autenticacao
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(string keyString)
        {
            // using (var provider = new RSACryptoServiceProvider(2048))
            // {
            //     Key = new RsaSecurityKey(provider.ExportParameters(true));
            // }
            // SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}