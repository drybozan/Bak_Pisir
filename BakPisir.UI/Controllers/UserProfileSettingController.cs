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
    public class UserProfileSettingController : Controller
    {
        UserService userService = new UserService();

        // GET: UserProfileSetting
        public ActionResult UserProfileSetting()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult ProfileSetting()
        {
             var profil = userService.GetSingleUser(1);
             ProfileSettingViewModel psvm = new ProfileSettingViewModel();
            psvm.userDto = profil;
            return PartialView(psvm);
        }

        [HttpPost]
        public string ProfileSetting(ProfileSettingViewModel viewModel)
        {
            //int id = SessionHelper.LoggedUserInfo.userId; ileryene zamanda giriş yapan id yi yükle
            int id = viewModel.userDto.userId;
            if (viewModel.ImageFile != null)
            {
                userService.UploadPicture(viewModel.ImageFile, id);
            }else if(viewModel.ImageFile == null)
            {
                 viewModel.userDto.profilePictureUrl = null;

            }

            userService.UpdateUser(id, viewModel.userDto);
            return "Bilgileriniz başarıyla kaydedilmiştir.";
        }

        [HttpGet]
        public PartialViewResult AccountSetting()
        {
            AccountSettingsViewModel accountSettingsViewModel = new AccountSettingsViewModel();
             var profil = userService.GetSingleUser(1);
             accountSettingsViewModel.userDto = profil;
            
            return PartialView(accountSettingsViewModel);
        }

        [HttpPost]
        public string AccountSetting(AccountSettingsViewModel accountSettingsViewModel)
        {

            //int id = SessionHelper.LoggedUserInfo.UserId;
            int id = accountSettingsViewModel.userDto.userId;
          
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