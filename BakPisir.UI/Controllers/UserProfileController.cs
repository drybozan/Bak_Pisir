using BakPisir.DTO.Dtos;
using BakPisir.UI.Models;
using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    public class UserProfileController : Controller
    {
         UserService userService = new UserService();
        RecipeService recipeService = new RecipeService();
        StepService stepService = new StepService();
        CategoryService categoryService = new CategoryService();
        SubCategoryService subCategoryService = new SubCategoryService();

        static int pageNumber = 1;
        static int pageSize = 9;

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

        //session bilgiisnden user id si alınacak ve parametre olarak verilecek
        [HttpGet]
        public ActionResult RecipeListByUser()
        {
            var recipes = recipeService.GetRecipeByUserId(1, pageNumber, pageSize);

            return View(recipes);
        }

        public PartialViewResult NavbarForUser(int id)
        {
            UserService userService = new UserService();
            UserDto user = userService.GetSingleUser(id);
            return PartialView("NavbarForUser",user);
        }


        [HttpGet]
        public ActionResult UpdateRecipe(int id)
        {
            var recipe = recipeService.GetSingleRecipe(id);
            var steps = stepService.GetStepByRecipeId(id);
            RecipeStepSubCategoryModelView recipeStepModelView = new RecipeStepSubCategoryModelView();
            recipeStepModelView.recipeDto = recipe;
            recipeStepModelView.stepDto = steps;

            var categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;

            var subCategories = subCategoryService.GetAllSubCategories();           
            ViewBag.SubCategories = subCategories;

            return View(recipeStepModelView);
        }

    }
}