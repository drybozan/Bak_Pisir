using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BakPisir.DTO.Dtos
{
    public class SubTransitionDto
    {
        [Display(Name = "Kategori Geçiş Id")]       
        public int categoryTransitionId { get; set; }

        [Display(Name = "Alt Kategori Id")]
        [Required]
        public int subCategoryId { get; set; }

        [Display(Name = "Tarif Id")]
        [Required]
        public int recipeId { get; set; }

        public bool isDelete { get; set; }

    }
}
