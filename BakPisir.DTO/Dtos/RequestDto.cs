
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BakPisir.DTO.Dtos
{
    public class RequestDto
    {
        [Display(Name = "Kullanıcı Id")]       
        public int requestId { get; set; }

        [Display(Name = "Kullanıcı İsmi")]
        [Required]
        [MaxLength(30, ErrorMessage = "Bu alana en fazla 30 karakter yazılabilir")]
        public string name { get; set; }

        [Display(Name = "Kullanıcı Soyismi")]
        [Required]
        [MaxLength(30, ErrorMessage = "Bu alana en fazla 30 karakter yazılabilir")]
        public string surname { get; set; }

        [Display(Name = "Kullanıcı Email")]
        [Required]
        [MaxLength(100, ErrorMessage = "Bu alana en fazla 100 karakter yazılabilir")]
        public string mail { get; set; }

        [Display(Name = "Kullanıcı Telefon No")]
        [Required]
        [MaxLength(12, ErrorMessage = "Bu alana en fazla 12 karakter yazılabilir")]
        public string phone { get; set; }

        [Display(Name = "Kullanıcı TC No")]
        [Required]
        [MaxLength(11, ErrorMessage = "Bu alana en fazla 11 karakter yazılabilir")]
        public string identityNumber { get; set; }

        [Display(Name = "Kullanıcı Doğum Tarihi")]
        [Required]
        public DateTime birthdate { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Bu alana en fazla 50 karakter yazılabilir")]
        public string username { get; set; }

        [Display(Name = "Kullanıcı Şifre")]
        [Required]
        [MaxLength(50, ErrorMessage = "Bu alana en fazla 50 karakter yazılabilir")]
        public string password { get; set; }

        [Required]
        public DateTime requestDate { get; set; }

        [Display(Name = "Kullanıcı Sözleşmesi")]
        [Required]
        public bool agreement { get; set; }
 

        public bool isDelete { get; set; }

    }
}
