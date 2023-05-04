using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace JWT.NetFramework.Helpers
{
    public class JwtAuthentication
    {
        private readonly string _secret;
        public JwtAuthentication(string secret)
        {
            _secret = secret;
            var keyBytes = Encoding.UTF8.GetBytes(_secret);
            _secret = Convert.ToBase64String(keyBytes);
            //_privateKey = new RSACryptoServiceProvider(2048);
        }

        public bool TryValidateToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;

            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_secret)),
                        ValidateIssuer = false,
                        ValidIssuer = "https://localhost:44317/",
                        ValidateAudience = false,                   
                        ValidAudience = "https://localhost:44317/",
                        /* ValidAudience özelliği, doğrulama işlemi sırasında kullanılan bir kuraldır. Bu özelliğin doğru bir şekilde ayarlanması, JWT token'ının doğru bir şekilde doğrulanmasına yardımcı olur. Eğer ValidAudience özelliği yanlış ayarlanırsa, doğrulama işlemi başarısız olur ve SecurityTokenInvalidAudienceException hatası fırlatılır.

Bu nedenle, JWT token'ının hedef kitle değerini belirledikten sonra, ValidAudience özelliğini doğru bir şekilde ayarlamalısınız.*/
                        ValidateLifetime = true
                    };

                    principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public string GenerateToken(ClaimsIdentity identity, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(_secret)), SecurityAlgorithms.HmacSha256Signature)
            });

            return handler.WriteToken(securityToken);
        }
    }
}