using BakPisir.DTO.Dtos;
using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    public class SignUpController : Controller
    {
        RequestService requestService= new RequestService();

        // GET: SignUp
        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.Msg = TempData["info"];
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(RequestDto requestDto)
        {
            //kayıt sayfasından alınan bilgilerden sonra kullanıcıya bir bilgi maili atılır başvurunun başarıyla alındığına dair.
            var info = requestService.SendMailInfo(requestDto.mail);
            TempData["info"] = info;

            //alınan bilgiler kayıt altına alınır.
            requestService.AddRequest(requestDto);
          
            return RedirectToAction("SignUp");
        }
    }
}