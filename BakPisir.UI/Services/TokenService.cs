using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class TokenService
    {
        //Gelen bilgileri token almak için gönderir ve tokeni döndürür.
        public TokenDto GetToken(string username, string password)
        {
            WebApiService<TokenDto> was = new WebApiService<TokenDto>();
            TokenDto token = was.GetToken(username, password);
            return token;
        }

        //Tokeni alan kullanıcı bilgilerini döndürür.
        public UserDto GetLoggedUser()
        {
            WebApiService<UserDto> was = new WebApiService<UserDto>();
            return was.GetSingle("LoginApi/Get");
        }
    }
}