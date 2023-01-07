using BakPisir.CORE.Helper;
using BakPisir.DTO.Dtos;
using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BakPisir.UI.Controllers
{
    public class SignInController : Controller
    {
        [HttpGet]
        // GET: SignIn
        // Bu action ile giriş yap arayüzü kullanıcıya döndürülür.
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        //Bu action ile arayüzden alınan bilgiler servise gönderilir.
        public ActionResult SignIn(UserDto model)
        {
            TokenService ts = new TokenService();
            var token = ts.CheckToken(model.username, model.password);
            if (token.access_token != null && token.expires_in != null && token.token_type != null)
            {
                SessionHelper.TokenInfo = token;
                SessionHelper.LoggedUserInfo = ts.GetLoggedUser();
                
                FormsAuthentication.SetAuthCookie(SessionHelper.LoggedUserInfo.username, false);
                return RedirectToAction("Member", "Member");
                
            }
            else
            {
                TempData["sign"] = "Kullanıcı adı veya şifre yanlış !!";
                return RedirectToAction("Signin");
            }
        }
    }
}