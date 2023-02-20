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
    public class UserHomePageController : Controller
    {

        /// <summary>
        /// Kullanıcı arayüzünde anasayfayı döndürür. Bu Action UserHomeView layoutunda  render edilmekte.
        /// </summary>
        /// 

        static int pageNumber = 1;
        static int pageSize = 10;
        [HttpGet]
        public ActionResult UserHomePage()
        {
            // RecipeService recipeService=new RecipeService();
            // var recipes = recipeService.GetAllRecipes(pageNumber,pageSize);
            // var recipes = recipes.Where(x => x.isDelete == false).ToList();
            return View();
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
        /// Listelenmiş kategorilerin alt kategorilerini dinamik olarak listelemek için SubMenu.cshtml dosyasına sorgu sonucu dataları gönderir.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public PartialViewResult SubMenu(int id)
        {
            SubCategoryService subCategoryService = new SubCategoryService();   
            List<SubCategoryDto> subCategories = subCategoryService.GetSubCategoryByCategoryId(id);
            return PartialView("SubMenu",subCategories);
        }

    }
}