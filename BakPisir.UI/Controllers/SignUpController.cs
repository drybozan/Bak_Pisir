using BakPisir.DTO.Dtos;
using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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
            //alınan bilgiler kayıt altına alınır.
            var message = requestService.AddRequest(requestDto);
            try
            {               
               
                TempData["info"] = message;
                //kayıt sayfasından alınan bilgilerden sonra kullanıcıya bir bilgi maili atılır başvurunun başarıyla alındığına dair.
                requestService.SendMailInfo(requestDto.mail);

            }
            catch (Exception)
            {

                TempData["info"] = message;
            }  
            return RedirectToAction("SignUp");
        }
    }
}