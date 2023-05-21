using BakPisir.CORE.Helper;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace BakPisir.API.Services
{
    public class TokenApiService
    {
        private static BakPisirDBEntities _dbContext = new BakPisirDBEntities();
        private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(_dbContext);

        //Girilen kullanıcı bilglerini veritabanı içerisinde kontrol eder.
        public UserDto CheckLogin(string username, string password)
        {
            var login = efUnitOfWork.UserTemplate.Get(x => x.username == username && x.password == password)
                .MapTo<UserDto>();
            return login;
        }

        //Token üzerindeki kimlik bilgilerini döndürür.
        public string TakeUserInfo(ClaimsIdentity idt)
        {
            UserDto user = new UserDto();
            var carry = idt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            user.userType = (carry[0].Equals("admin")) ? true : (carry[0].Equals("user")) ? false : false;
            user.username = idt.Name;
            user.registrationDate = Convert.ToDateTime(idt.Claims.FirstOrDefault(c => c.Type == "LoggedOn").Value);
            user.userId = Convert.ToInt32(idt.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                

            return JsonConvert.SerializeObject(user);
        }
    }
}