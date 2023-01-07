using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BakPisir.CORE.Helper
{
    /// <summary>
    /// Session(oturum) Yardımcı Nesnesi.
    /// </summary>
    public class SessionHelper
    {

        /// <summary>Giriş yapmış kullanıcı kimlik bilgisi</summary>
        public static UserDto LoggedUserInfo
        {
            //HttpContext dosyasında UserInfo nesnesi null dönerse bir UserDto nesnesi oluştur.

            get => (UserDto)HttpContext.Current.Session["UserInfo"] ?? new UserDto();
            set => HttpContext.Current.Session["UserInfo"] = value;
        }

        /// <summary>Token bilgisi</summary>
        public static TokenDto TokenInfo
        {
            //HttpContext dosyasında TokenInfo nesnesi null dönerse bir TokenDto nesnesi oluştur.
            get => (TokenDto)HttpContext.Current.Session["TokenInfo"] ?? new TokenDto();
            set => HttpContext.Current.Session["TokenInfo"] = value;
        }



        public static Guid? SubcontractorGuid
        {
            get => (Guid?)HttpContext.Current.Session["Guid"];
            set => HttpContext.Current.Session["Guid"] = value;
        }

        public static bool? YardiPass
        {
            get => (bool?)HttpContext.Current.Session["YardiPass"];
            set => HttpContext.Current.Session["YardiPass"] = value;
        }


    }
}
