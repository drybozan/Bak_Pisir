using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Models
{
    public class ProfileSettingViewModel
    {
        public UserDto userDto { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}