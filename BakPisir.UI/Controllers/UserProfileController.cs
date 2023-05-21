using BakPisir.CORE.Filter;
using BakPisir.CORE.Helper;
using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using BakPisir.UI.Models;
using BakPisir.UI.Services;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakPisir.UI.Controllers
{
    [LogAction]
    public class UserProfileController : Controller
    {
        UserService userService = new UserService();
        RecipeService recipeService = new RecipeService();
        StepService stepService = new StepService();
        CategoryService categoryService = new CategoryService();
        SubCategoryService subCategoryService = new SubCategoryService();
        SubTransitionService subTransitionService = new SubTransitionService();


        static int pageNumber = 1;
        static int pageSize = 9;

     
        List<HttpPostedFileBase> ImageFileforStep = new List<HttpPostedFileBase>();
        int returnRecipeId;


       [HttpGet]
        public ActionResult UserProfile(int id)
        {
          
            var user = userService.GetSingleUser(id);
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
        public ActionResult RecipeListByUser(int id)
        {
            var recipes = recipeService.GetRecipeByUserId(id, pageNumber, pageSize);

            return View(recipes);
        }

        [HttpGet]
        public JsonResult getNextRecipeListByUser(int number)
        {
            int id = SessionHelper.LoggedUserInfo.userId; 

            RecipeListModel recipes = recipeService.GetRecipeByUserId(id, number, pageSize);
            return Json(recipes, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult NavbarForUser(int id)
        {
            UserService userService = new UserService();
            UserDto user = userService.GetSingleUser(id);
            return PartialView("NavbarForUser",user);
        }

        [HttpPost]
        public string DeleteRecipe(int id)
        {
            recipeService.DeleteRecipe(id);
            return "Tarifiniz silindi";
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


        [HttpPost]
        public string UpdateRecipe(RecipeStepSubCategoryModelView recipeStepSubCategoryModelView)
        {
            int id = SessionHelper.LoggedUserInfo.userId; 
            int recipeId = recipeStepSubCategoryModelView.recipeDto.recipeId;
            recipeStepSubCategoryModelView.recipeDto.userId = id;

            recipeService.UpdateRecipe(recipeId, recipeStepSubCategoryModelView.recipeDto);
            if (recipeStepSubCategoryModelView.ImageFile[0] != null)
            {
                recipeService.UploadRecipePicture(recipeId, recipeStepSubCategoryModelView.ImageFile[0]);
             
            }
            if (recipeStepSubCategoryModelView.ImageFile[recipeStepSubCategoryModelView.ImageFile.Count-1] != null)
            {
                recipeService.UploadRecipeVideo(recipeId, recipeStepSubCategoryModelView.ImageFile[recipeStepSubCategoryModelView.ImageFile.Count-1]);

            }


            for (int i = 0; i< recipeStepSubCategoryModelView.stepDto.Count; i++)
            {
               int stepId = recipeStepSubCategoryModelView.stepDto[i].stepId;
                stepService.UpdateStep(stepId, recipeStepSubCategoryModelView.stepDto[i]);
                if (recipeStepSubCategoryModelView.ImageFile[i+1] != null)
                {
                    stepService.UploadStepPicture(stepId, recipeStepSubCategoryModelView.ImageFile[i+1]);

                }

            }

            SubTransitionDto subTransiton = subTransitionService.GetSubCategoryByRecipeId(recipeId);
            int categoryTransitionId = subTransiton.categoryTransitionId; 
            int subCategoryId = recipeStepSubCategoryModelView.subCategoryDto.subCategoryId;

            SubTransitionDto subTransitionDto = new SubTransitionDto();
            subTransitionDto.recipeId = recipeId;
            subTransitionDto.subCategoryId = subCategoryId;
            subTransitionDto.isDelete = false;

            subTransitionService.UpdateSubTransition(categoryTransitionId, subTransitionDto);


            return "Tarifiniz Başarıyla Güncellendi.";
        }

        [HttpGet]
        public ActionResult AddRecipe()
        {
            var categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;

            var subCategories = subCategoryService.GetAllSubCategories();
            ViewBag.SubCategories = subCategories;

            return View();
        }

        [HttpPost]
        public string AddRecipe(RecipeStepSubCategoryModelView recipeStepSubCategoryModelView)
        {
            // StepDto listesini alma
            var stepDtoListJson = Request.Form["stepDto"];
            var stepDtoList = JsonConvert.DeserializeObject<List<StepDto>>(stepDtoListJson);

            recipeStepSubCategoryModelView.recipeDto.userId = SessionHelper.LoggedUserInfo.userId;
            returnRecipeId = recipeService.AddRecipe(recipeStepSubCategoryModelView.recipeDto);


            //recipeImage kaydı yapar
            if (recipeStepSubCategoryModelView.ImageFile[0] != null)
            {
                recipeService.UploadRecipePicture(returnRecipeId, recipeStepSubCategoryModelView.ImageFile[0]);

            }
            //recipeVideo kaydı yapar
            if (recipeStepSubCategoryModelView.ImageFile[recipeStepSubCategoryModelView.ImageFile.Count - 1] != null)
            {
                recipeService.UploadRecipeVideo(returnRecipeId, recipeStepSubCategoryModelView.ImageFile[recipeStepSubCategoryModelView.ImageFile.Count - 1]);

            }

          
                // step resimlerini listeye toplar
           for (int i = 0; i < recipeStepSubCategoryModelView.ImageFile.Count - 2; i++)
             {
                    ImageFileforStep.Add(recipeStepSubCategoryModelView.ImageFile[i + 1]);

           }
           

            int returnStepId;
            for (int i = 0; i < stepDtoList.Count; i++)
            {
                stepDtoList[i].recipeId = returnRecipeId;
                returnStepId = stepService.AddStep(stepDtoList[i]);
                stepService.UploadStepPicture(returnStepId, ImageFileforStep[i]);
            }
            SubTransitionDto subTransitionDto = new SubTransitionDto();
            subTransitionDto.recipeId = returnRecipeId;
            subTransitionDto.subCategoryId = recipeStepSubCategoryModelView.subCategoryDto.subCategoryId;
            subTransitionDto.isDelete = false;

            subTransitionService.AddSubTransition(subTransitionDto);

            return "Tarifiniz Başarıyla Kaydedildi";
        }

 
    }
}