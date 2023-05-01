using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Models
{
    public class RecipeSubTransitionViewModel
    {
        public RecipeDto recipeDto { get; set; }
        public List<SubTransitionDto> subTransitionDto { get; set; }
    }
}