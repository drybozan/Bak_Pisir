using BakPisir.DTO.Dtos;
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
        public List<UserDto> GetAllUsers(int page, int pageSize)
        {
            return was.Get("UserApi/GetAll",page,pageSize);
        }

        //Verilen id değerine sahip user verisini çeker.
        public UserDto GetSingleUser(int id)
        {
            var user = was.GetId("UserApi/Get?id=", id);
            return user;
        }

        //Verilen id değerine sahip user verisini veritabanından siler.
        public void DeleteUser(int id)
        {
            was.Delete("UserApi / Delete", id);
        }

        //Yeni user ekler.
        public void AddUser(UserDto value)
        {
            was.Add("UserApi / Add", value);
        }

        //Verilen id değerine sahip user verisini günceller.
        public void UpdateUser(int id, UserDto value)
        {
            was.Update("UserApi / Update", id, value);
        }

        public void UploadPicture(HttpPostedFileBase file, int id)
        {
            was.UploadFile("UserApi / UploadProfilePicture", id, file);
        }


        public string SendMailPassword(string mail)
        {
            return was.GetMailFeedback("UserApi / SendMail", mail);
        }
    }
}