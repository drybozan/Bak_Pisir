using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BakPisir.DTO.Dtos
{
    public class LogDto
    {
        [Display(Name = "Log Id")]
        public int logId { get; set; }

        [Display(Name = "Kullanıcı Id")]
        [Required]
        public int userId { get; set; }

        [Display(Name = "Log Aktivite")]
        [Required]
        public string logActivity { get; set; }

        [Display(Name = "Log Tarihi")]
        [Required]
        public DateTime logDate { get; set; }

        [Display(Name = "Log Ip Adresi")]
        [Required]
        [MaxLength(16, ErrorMessage = "Bu alana en fazla 16 karakter yazılabilir")]
        public string ipAddress { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required]
        [MaxLength(50, ErrorMessage = "Bu alana en fazla 50 karakter yazılabilir")]
        public string logUsername { get; set; }
    }
}
