using BakPisir.DTO.Dtos;
using BakPisir.UI.Services;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ViewBag.Mail = TempData["mail"];
            return View();
        }
        // POST: ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(UserDto userDto)
        {
            UserService userService = new UserService();
            TempData["mail"] = userService.SendMailPassword(userDto.mail);           
            return RedirectToAction("ForgotPassword");
        }
    }
}