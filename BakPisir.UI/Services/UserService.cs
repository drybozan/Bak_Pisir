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
        WebApiService<UserListModel> _was = new WebApiService<UserListModel>();

        //Bütün user tablosunu çeker.
        public UserListModel GetAllUsers(int page, int pageSize)
        {
            return _was.Get("UserApi/GetAll", page, pageSize);
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
    }
}