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
        // GET: UserHomePage
        [HttpGet]
        public ActionResult UserHomePage()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Menu()
        {
            CategoryService categoryService = new CategoryService();
            var categories = categoryService.GetAllCategories();
            return PartialView("Menu", categories);

        }

        [HttpGet]
        public PartialViewResult SubMenu(int id)
        {
            SubCategoryService subCategoryService = new SubCategoryService();   
            List<SubCategoryDto> subCategories = subCategoryService.GetSubCategoryByCategoryId(id);
            return PartialView("SubMenu",subCategories);
        }

    }
}