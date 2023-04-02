using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    public class UserProfileController : Controller
    {
         UserService userService = new UserService();
        [HttpGet]
        public ActionResult UserProfile()
        {
          
            var user = userService.GetSingleUser(1);
            return View(user);
        }

        /// <summary>
        /// UserProfile.cshtml de hesap silme formu için tetiklenmekte.CheckBox da yapılan seçimle kullanıcını hesabını siler.
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void userIsDelete(int id)
        {
            userService.DeleteUser(id);
        }
    }
}