using BakPisir.CORE.Filter;
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
    public class AdminController : Controller
    {

        RequestService requestService = new RequestService();
        UserService userService = new UserService();
        CategoryService categoryService = new CategoryService();
        SubCategoryService subCategoryService = new SubCategoryService();
        LogService logService = new LogService();

        // GET: Admin
        public ActionResult AdminHomePage()
        {
            return View();
        }



        public ActionResult Requests()
        {
            ViewBag.Msg = TempData["info"];
            return View();
        }

        //listelenecek üyeleri tablodan çeker ve view tarafında listelenir.
        [HttpGet]
        public JsonResult GetRequest()
        {
            var requests = requestService.GetAllRequest();
            return Json(requests, JsonRequestBehavior.AllowGet);
        }


        //Admin tarafından başvurusu kabul edilmeyen başvurular isDelete flagı true yapılır ve kullanıcya bilgi mesajı gönderilir.
        public void DeleteRequest(int id)
        {
            var requestDto = requestService.GetSingleRequest(id);
            var message = requestService.SendMailRejection(requestDto.mail);
            TempData["info"] = message;
            requestService.DeleteRequest(id);
           
        }

        //Admin tarafından başvurusu onaylanan kullanıcının id siden tüm bilgilerini çeker. 
        //Başvuru onayı aldıktan sonra bir şifre gönderilir mail ile kullanıcıya.
        //Başvuru tablosundan kullanıcı bilgileri alınır üye tablosuna kaydedilir ve son olarak başvuru tablosundan ilgili id'li başvuru silinir.
        public void AcceptRequest(int id)
        {
            var requestDto = requestService.GetSingleRequest(id);
            var message = requestService.SendMailPassword(requestDto.mail);
           

            var takePassword = requestService.GetSingleRequest(id).password;
            UserDto userDto = new UserDto();
            userDto.name = requestDto.name;
            userDto.surname = requestDto.surname;
            userDto.username = requestDto.username;
            userDto.phone = requestDto.phone;
            userDto.mail = requestDto.mail;
            userDto.registrationDate = DateTime.Now;
            userDto.userType = false;
            userDto.isDelete = false;
            userDto.password = takePassword;

            userService.AddUser(userDto);
            requestService.DeleteRequest(requestDto.requestId);
            TempData["info"] = message;
        }


        public ActionResult Users()
        {
            return View();
        }


        // Silinecek üyeler id ile gönderilir ve tablodan silinir.
        public void DeleteUser(int id)
        {
           var message =  userService.DeleteUser(id);
       
        }

        //listelenecek üyeleri tablodan çeker ve view tarafında listelenir.
        [HttpGet]
        public JsonResult GetUsers()
        {
            var users = userService.GetAllUsers();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //Pasif user ı aktif yapar
        public void ActiveUser(int id)
        {
            userService.ActiveUser(id);
            var mail = userService.GetSingleUser(id).mail;
            userService.SendMailActiveUser(mail);
        }


        public ActionResult Log()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetLogs()
        {
            var logs = logService.GetAllLogs();
            return Json(logs, JsonRequestBehavior.AllowGet);
        }


        public void DeleteLog(int id)
        {
            logService.DeleteLog(id);
        }

        [HttpGet]

        public ActionResult Category()
        {
            List<CategoryDto> categories = new List<CategoryDto>();
            categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        //listelenecek üyeleri tablodan çeker ve view tarafında listelenir.
        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = categoryService.GetAllCategories();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AddCategory(string categoryName)
        {
            CategoryDto categoryDto = new CategoryDto();
            categoryDto.categoryName = categoryName;
            categoryService.AddCategory(categoryDto);
            return "Başarılı"; // başarılı sonuç döndürülür
        }

        [HttpPost]
        public string UpdataCategory(int categoryId, string categoryName)
        {
            CategoryDto categoryDto = new CategoryDto();
            categoryDto.categoryId = categoryId;
            categoryDto.categoryName = categoryName;
            categoryService.UpdateCategory(categoryId, categoryDto);

            return "Başarılı"; // başarılı sonuç döndürülür
        }

        // Silinecek üyeler id ile gönderilir ve tablodan silinir.
        public void DeleteCategory(int id)
        {
            categoryService.DeleteCategory(id);
        }


        [HttpGet]
        public JsonResult GetSubCategories()
        {
            var subCategories = subCategoryService.GetAllSubCategories();
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public string AddSubCategory(int categoryId, string subCategoryName)
        {

            SubCategoryDto subCategoryDto = new SubCategoryDto();
            subCategoryDto.subCategoryName = subCategoryName;
            subCategoryDto.categoryId = categoryId;
            subCategoryService.AddSubCategory(subCategoryDto);
            return "Başarılı"; // başarılı sonuç döndürülür
        }

        [HttpPost]
        public string UpdataSubCategory(int subCategoryId, string subCategoryName, int categoryId)
        {
            SubCategoryDto subCategoryDto = new SubCategoryDto();
            subCategoryDto.subCategoryId = subCategoryId;
            subCategoryDto.categoryId = categoryId;
            subCategoryDto.isDelete = false;
            subCategoryDto.subCategoryName = subCategoryName;
            subCategoryService.UpdateSubCategory(subCategoryId, subCategoryDto);
            return "Başarılı"; // başarılı sonuç döndürülür
        }

        // Silinecek üyeler id ile gönderilir ve tablodan silinir.
        public void DeleteSubCategory(int id)
        {
            subCategoryService.DeleteSubCategory(id);
        }


    }
}