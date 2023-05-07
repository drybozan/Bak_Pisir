using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Models
{
    public class RecipeStepSubCategoryModelView
    {
        public RecipeDto recipeDto { get; set; }
        public List<StepDto> stepDto { get; set; }
        public SubCategoryDto subCategoryDto { get; set; }
        public List<HttpPostedFileBase> ImageFile { get; set; }
    }
}
