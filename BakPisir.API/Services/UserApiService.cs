using AutoMapper.QueryableExtensions;
using BakPisir.CORE.Helper;
using BakPisir.CORE.Paging;
using BakPisir.CORE.Result;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace BakPisir.API.Services
{
    public class UserApiService
    {
        // Önce Database Context dosyamın nesnesini oluşturuyorum.
        private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

        // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
        private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

        public string GetAllUser(int page, int pageSize)
        {
            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
            var users = efUnitOfWork.UserTemplate.GetAll(i => i.isDelete == false)
            .OrderBy(o => o.userId) // gelen datayı id ye göre sırala
            .ProjectTo<UserDto>()
            .ToList()
            .ToPaginate(page, pageSize);


            UserListModel mappedUserListModel = users.MapTo<UserListModel>();
            return JsonConvert.SerializeObject(mappedUserListModel);
        }

        //Verilen id değerine sahip user verisini veritabanında bulur ve döndürür.
        public string GetUserById(int id)
        {
            var user = efUnitOfWork.UserTemplate.GetById(id).MapTo<UserDto>();
            return JsonConvert.SerializeObject(user);
        }

        //Verilen id değerine sahip user verisini veritabanından siler.
        public Result DeleteUser(int id)
        {
            var result = Result.Instance.Warning("HATA! Böyle bir kullanıcı yok.");
            var user = efUnitOfWork.UserTemplate.GetById(id);
            if (user != null)
            {
                efUnitOfWork.UserTemplate.Delete(user);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla silindi.");
                return result;
            }

            return result;
        }

        //Yeni kullanıcı ekler.
        public Result AddUser(UserDto userDto)
        {
            var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

            if (userDto != null) //girilen bilgilerin kontrolü yapılır.
            {
                var mappedUser = userDto.MapTo<UserTBL>();
                mappedUser.registrationDate= DateTime.Now;
                efUnitOfWork.UserTemplate.Add(mappedUser);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Kullanıcı başarıyla eklendi.");

                return result;
            }
            return result;
        }

        //Verilen id değerine sahip kullanıcıyı günceller.
        public Result UpdateUser(int id, UserDto userDto)
        {
            var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

            // istenilen id mevcutsa güncellenecek data user a atanır.
            var user = efUnitOfWork.UserTemplate.GetById(id).MapTo<UserDto>();
            if (user != null)
            {
                var mapped = userDto.MapTo<UserTBL>();
                mapped.userId = user.userId;

                efUnitOfWork.UserTemplate.Update(mapped);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Kullanıcı başarıyla güncellendi.");
                return result;
            }
            return result;
        }

        // şifremi unuttum
        public Result SendMailPassword(object mail)
        {
            var result = Result.Instance.Warning("HATA! Kayıtlı email adresi bulunamadı.");
            var user = efUnitOfWork.UserTemplate.Get(x => x.mail == mail).MapTo<UserDto>();
            if (user != null)
            {
                Guid randomkey = Guid.NewGuid(); //32 karakterli kodu ürettik
                string Password = randomkey.ToString().Substring(0, 6); /// 6 haneli keyi password için aldık"
                Mail sender = new Mail();
                sender.sendMail(mail.ToString(), "Yeni Şifre", "Yeni Şifrenizle giriş yapabilirsiniz.\n Şifre : " + Password);
                user.password = Password;
                UpdateUser(user.userId, user);

                result = Result.Instance.Success("Yeni şifreniz email adresinize yollandı.");
                return result;
            }
            else
            {
                return result;
            }
        }

      
        public Result UploadProfilePicture(int id, string filePath)
        {
            var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

            // gelen id yi sorgula db de sorgula. Bu id ile kullanıcı kayıtlı ise onu UserDto entity ile maple.
            var user = efUnitOfWork.UserTemplate.GetById(id).MapTo<UserDto>();

            if (user != null)
            {
                //kullanıcının profil fotoğrafına gönderilen dosyanın yolunu gönder.
                user.profilePictureUrl = filePath;

                //kullanıcıyı veritabanı nesnesi ile maple.
                var mappedUser = user.MapTo<UserTBL>();
                // yapılan değişikliği güncelle
                efUnitOfWork.UserTemplate.Update(mappedUser);

                // değişiklikleri kaydet ve veritabanına yansıt
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Kullanıcı profili başarıyla güncellendi.");
                return result;
            }
            return result;

        }

    }
}
