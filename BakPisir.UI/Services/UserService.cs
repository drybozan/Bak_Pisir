using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class UserService
    {
        WebApiService<UserDto> was = new WebApiService<UserDto>();

        //Bütün user tablosunu çeker.
        public List<UserDto> GetAllUsers()
        {
            return was.GetAll("UserApi/GetAll");
        }


        //Verilen id değerine sahip user verisini veritabanından siler.
        public String ActiveUser(int id)
        {
            return was.ActiveUser("UserApi/ActiveUser", id);
        }

        //Verilen id değerine sahip user verisini çeker.
        public UserDto GetSingleUser(int id)
        {
            var user = was.GetId("UserApi/Get", id);
            return user;
        }

        //Verilen id değerine sahip user verisini veritabanından siler.
        public String DeleteUser(int id)
        {
           return was.Delete("UserApi/Delete", id);
        }

        //Yeni user ekler.
        public String AddUser(UserDto value)
        {
            return was.Add("UserApi/Add", value);
        }

        //Verilen id değerine sahip user verisini günceller.
        public String UpdateUser(int id, UserDto value)
        {
           return was.Update("UserApi/Update", id, value);
        }

        public String UploadPicture(HttpPostedFileBase file, int id)
        {
           return was.UploadFile("UserApi/UploadProfilePicture", id, file);
        }


        public string SendMailPassword(string mail)
        {
            return was.GetMailFeedback("UserApi/SendMail", mail);
        }


        public string SendMailActiveUser(string mail)
        {
            return was.GetMailFeedback("UserApi/SendMailActiveUser", mail);
        }
    }
}