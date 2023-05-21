using BakPisir.CORE.Filter;
using BakPisir.CORE.Helper;
using BakPisir.UI.Models;
using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    [LogAction]
    public class AdminProfileSettingController : Controller
    {
        UserService userService = new UserService();

        [HttpGet]
        public ActionResult AdminProfile(int id)
        {
             
            var user = userService.GetSingleUser(id);
            return View(user);
        }

        /// <summary>
        /// UserProfile.cshtml de hesap silme formu için tetiklenmekte.CheckBox da yapılan seçimle kullanıcını hesabını siler.
        /// </summary>
        /// <param name="id"></param>

        [HttpGet]
        public ActionResult AdminProfileSetting()
        {
            return View();
        }


        [HttpGet]
        public PartialViewResult AdmnProfileSetting(int id)
        {
            var profil = userService.GetSingleUser(id);
            ProfileSettingViewModel psvm = new ProfileSettingViewModel();
            psvm.userDto = profil;
            return PartialView(psvm);
        }

        [HttpPost]
        public string ProfileSetting(ProfileSettingViewModel viewModel)
        {
            int id = SessionHelper.LoggedUserInfo.userId;
           
            userService.UpdateUser(id, viewModel.userDto);

            if (viewModel.ImageFile != null)
            {
                userService.UploadPicture(viewModel.ImageFile, id);

            }
            return "Bilgileriniz başarıyla kaydedilmiştir.";
        }

        [HttpGet]
        public PartialViewResult AdminAccountSetting(int id)
        {
            AccountSettingsViewModel accountSettingsViewModel = new AccountSettingsViewModel();
            var profil = userService.GetSingleUser(id);
            accountSettingsViewModel.userDto = profil;

            return PartialView(accountSettingsViewModel);
        }

        [HttpPost]
        public string AccountSetting(AccountSettingsViewModel accountSettingsViewModel)
        {

            int id = SessionHelper.LoggedUserInfo.userId;
           

            if (accountSettingsViewModel.oldPassword == accountSettingsViewModel.userDto.password)
            {
                if (accountSettingsViewModel.newPassword1 != null && accountSettingsViewModel.newPassword2 != null)
                {

                    accountSettingsViewModel.userDto.password = accountSettingsViewModel.newPassword1;

                }
                userService.UpdateUser(id, accountSettingsViewModel.userDto);
                return "Bilgileriniz başarıyla kaydedilmiştir.";
            }
            else
            {

                return "Girdiğiniz şifre yanlış!!";

            }
        }

    }
}