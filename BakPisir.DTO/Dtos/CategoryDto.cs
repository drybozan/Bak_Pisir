using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BakPisir.DTO.Dtos
{
    public class CategoryDto
    {
        [Display(Name = "Kategori Id")]
        public int categoryId { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Bu alana en fazla 50 karakter yazılabilir")]
        public string categoryName { get; set; }

        public bool isDelete { get; set; }    
    }
}
