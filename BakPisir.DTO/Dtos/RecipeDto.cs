using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BakPisir.DTO.Dtos
{
    public class RecipeDto
    {
        [Display(Name = "Tarif Id")]        
        public int recipeId { get; set; }

        [Display(Name = "Kullanıcı Id")]
        [Required]
        public int userId { get; set; }

        [Display(Name = "Kategori Id")]
        [Required]
        public int categoryId { get; set; }

        [Display(Name = "Tarif Adı")]
        [Required]
        public string recipeName { get; set; }

        [Display(Name = "Tarif Resmi")]
        [Required]
        public string recipeImageUrl { get; set; }

        [Display(Name = "Tarif Video Url")]
        [Required]
        public string recipeVideoUrl { get; set; }

        [Display(Name = "İçindekiler")]
        [Required]
        public string ingredients { get; set; }


        [Display(Name = "Pişirme süresi")]
        [Required]
        [MaxLength(50, ErrorMessage = "Bu alana en fazla 50 karakter yazılabilir")]
        public string cookingTime { get; set; }

        [Display(Name = "Tarif Tarihi")]
        [Required]
        public DateTime recipeDate { get; set; }        

        public bool isDelete { get; set; }


    }
}
