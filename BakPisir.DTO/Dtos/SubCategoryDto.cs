using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BakPisir.DTO.Dtos
{
    public class SubCategoryDto
    {
        [Display(Name = "Alt Kategori Id")]
        public int subCategoryId { get; set; }

        [Display(Name = "Kategori Id")]
        [Required]
        public int categoryId { get; set; }

        [Display(Name = "Tarif Id")]
        [Required]
        public int recipeId { get; set; }

        [Display(Name = "Alt Kategori Adı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Bu alana en fazla 50 karakter yazılabilir")]
        public string subCategoryName { get; set; }

        public bool isDelete { get; set; }

    }
}
