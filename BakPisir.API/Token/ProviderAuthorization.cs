using BakPisir.API.Services;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BakPisir.API.Token
{
    public class ProviderAuthorization : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //Kullanıcı bilgilerinin boş olup olmadığını kontrol eder.
            if (context.UserName != null && context.Password != null)
            {
                //Kullanıcı bilgileri servis üzerinden kontrol eder.
                TokenApiService tokenService = new TokenApiService();
                var loginData = tokenService.CheckLogin(context.UserName, context.Password);
                if (loginData != null)
                {
                    //Kullanıcı tipinin karşılığı ayarlar.
                    string userType = "";
                    if (loginData.userType == true)
                    {
                        userType = "admin";

                    }
                    else if (loginData.userType == false)
                    {
                        userType = "user";
                    }

                    //Kullanıcı bilgilerine göre kimlik oluşturur.
                    ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Role, userType));
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    identity.AddClaim(new Claim("UserId", loginData.userId.ToString()));
                    identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("hata", "Kullanıcı adı veya şifre hatalı.");
                }
            }
            else
            {
                context.SetError("hata", "Kullanıcı adı veya şifre boş olamaz.");
            }

        }
    }
}