﻿using BakPisir.CORE.Filter;
using BakPisir.CORE.Helper;
using BakPisir.DTO.Dtos;
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
    public class UserProfileSettingController : Controller
    {
        UserService userService = new UserService();

      
        public ActionResult UserProfileSetting()
        {
            return View();
        }

       
        [HttpGet]
        public PartialViewResult ProfileSetting(int id)
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
        public PartialViewResult AccountSetting(int id)
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