using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Models
{
    public class MenuModelView
    {
       public List<CategoryDto> categoryDto { get; set; }
       public List<SubCategoryDto> subCategoryDto { get; set; }
    }
}