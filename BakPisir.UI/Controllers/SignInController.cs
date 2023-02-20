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
            ViewBag.Msg = TempData["login"];
            return View();
        }

        [HttpPost]
        //Bu action ile arayüzden alınan bilgiler servise gönderilir.
        public ActionResult SignIn(UserDto model)
        {
            TokenService tokenService = new TokenService();
            //kullanıcıdan alınan username ve password bilgisi ile token alıyoruz.
            TokenDto token = tokenService.GetToken(model.username, model.password);
            if (token.access_token != null && token.expires_in != null && token.token_type != null)
            {
                //token bilgilerini session bilgisine atıyoruz.
                SessionHelper.TokenInfo = token;

                //token üzerinden kullanıcının bilgilerini çekip session bilgisine atıyoruz.
                SessionHelper.LoggedUserInfo = tokenService.GetLoggedUser();
                
                //cookie'ye session üzerinden çektiğimiz kullanıcı bilgilerini ekliyoruz.
                FormsAuthentication.SetAuthCookie(SessionHelper.LoggedUserInfo.username, false);

                return RedirectToAction("anasayafaya gidecek", "Member");
                
            }
            else
            {
                TempData["login"] = "Kullanıcı adı veya şifre yanlış !!";
                return RedirectToAction("SignIn");
            }
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return View("Signin");
        }
    }
}