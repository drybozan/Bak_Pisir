using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    public class UserHomePageController : Controller
    {

        /// <summary>
        /// Kullanıcı arayüzünde anasayfayı döndürür. Bu Action UserHomeView layoutunda  render edilmekte.
        /// </summary>
        /// 
        RecipeService recipeService = new RecipeService();

        static int pageNumber = 1;
        static int pageSize = 3;
        [HttpGet]
        public ActionResult UserHomePage()
        {
            RecipeListModel recipes = recipeService.GetAllRecipes(pageNumber, pageSize);

            return View(recipes);
        }



        //UserHomeView üzerinden JQuery ile sayfa numarası verilir. Sorgu sonucu burdan veriler Json formatından viewa gider. getNextRecipe Fonsiyonu ile data yerleştirlir.
        [HttpGet]
        public JsonResult getNextRecipe(int id)
        {
            RecipeListModel recipes = recipeService.GetAllRecipes(id, pageSize);
            return Json(recipes, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        //Kullanıcı arayüzünde sol tarafta dinamik olarak kategorileri listelemek için bir partialview render eder.Menu.cshtml dosyasına sorgu sonucu gelen dataları gönderir. 
        /// </summary>

        [HttpGet]
        public PartialViewResult Menu()
        {
            CategoryService categoryService = new CategoryService();
            var categories = categoryService.GetAllCategories();
            return PartialView("Menu", categories);

        }

        /// <summary>
        /// Menu.cshtml üzerinden seçilen categoryId ile sorgu yapar. Dönen sonucu Json formatında menu.cshtml'deki getCategoryId(id) scriptine teslim eder. Data Jquery sayfayı yenilemeden yerleştirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public JsonResult GetAllRecipeByCategoryId(int id)
        {
            RecipeListModel recipes = recipeService.GetRecipeByCategoryId(id, pageNumber, pageSize);

            return Json(recipes, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Listelenmiş kategorilerin alt kategorilerini dinamik olarak listelemek için SubMenu.cshtml dosyasına sorgu sonucu dataları gönderir.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public PartialViewResult SubMenu(int id)
        {
            SubCategoryService subCategoryService = new SubCategoryService();
            List<SubCategoryDto> subCategories = subCategoryService.GetSubCategoryByCategoryId(id);
            return PartialView("SubMenu", subCategories);
        }

        /// <summary>
        /// Kategori id sine göre tarif getirir. UserHomePage içine çizer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllRecipeBySubCategoryId(int id)
        {
            SubTransitionService subTransitionService = new SubTransitionService();
            var recipes = subTransitionService.GetSubTransitionBySubCategoryId(id, pageNumber, pageSize);

            return Json(recipes, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Seçilen tarifin detayını getirir. RecipeDetail View ın içine listeler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RecipeDetail(int id)
        {
            var singleRecipe = recipeService.GetSingleRecipe(id);
            return View(singleRecipe);
        }

        /// <summary>
        /// Tarifin detayı listelenirken ilgili tarifin adımalrını RecipeDetailin içindeki partialview içine listeler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult StepsRecipe(int id)
        {
            StepService stepService = new StepService();
            List<StepDto> steps = stepService.GetStepByRecipeId(id);
            return PartialView("StepsRecipe", steps);
        }

        /// <summary>
        /// profili görüntüle diyince. İlgili kullanıcının profilini ve tariflerini çizer.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult VisitedProfile(int id)
        {
            var recipes = recipeService.GetRecipeByUserId(id, pageNumber, pageSize);
            return View(recipes);
        }

        [HttpGet]
        public PartialViewResult UserInfo(int id)
        {
            UserService userService = new UserService();
            UserDto user = userService.GetSingleUser(id);
            return PartialView("UserInfo", user);

        }
    }
}