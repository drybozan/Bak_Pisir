using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakPisir.DTO.Dtos
{
    public class StepDto
    {
        [Display(Name = "Tarif Adım Id")]
        public int stepId { get; set; }

        [Display(Name = "Tarif Id")]
        [Required]
        public int recipeId { get; set; }

        [Display(Name = "Tarif Adımı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Bu alana en fazla 50 karakter yazılabilir")]
        public string stepName { get; set; }

        [Display(Name = "Tarif Adımı Detay")]
        [Required]
        public string stepDescription { get; set; }

        [Display(Name = "Tarif Adımı Fotoğraf")]
        [Required]
        public string stepImageUrl { get; set; }


        public bool isDelete { get; set; }


    }
}
