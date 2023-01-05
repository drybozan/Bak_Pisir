using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BakPisir.DTO.Dtos
{
    public class CommentDto
    {
        [Display(Name = "Yorum Id")]
       
        public int commentId { get; set; }

        [Display(Name = "Tarif Id")]
        [Required]
        public int recipeId { get; set; }

        [Display(Name = "Kullanıcı Id")]
        [Required]
        public int userId { get; set; }

        [Display(Name = "Yorum")]
        [Required]
        [MaxLength(500, ErrorMessage = "Bu alana en fazla 500 karakter yazılabilir")]
        public string comment { get; set; }

        [Required]
        public int parentId { get; set; }

        [Display(Name = "Yorum Fotoğraf")]
        [Required]
        public string commentImageUrl { get; set; }

        public bool isDelete { get; set; }

        [Display(Name = "Yorum Tarihi")]
        [Required]
        public DateTime commentDate { get; set; }

    }
}
