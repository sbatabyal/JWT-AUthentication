using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationApp.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "Preskriptor";
        public static string Issuer { get; } = "Authentication API";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(40); //To Do : Change/Remove
        public static string TokenType { get; } = "Bearer";
    }
}
