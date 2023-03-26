using BakPisir.API.Services;
using BakPisir.CORE.Result;
using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BakPisir.API.Controllers
{
    //[Authorize]    
    public class UserApiController : ApiController
    {
        private UserApiService _userApiService = new UserApiService();

        [Route("api/UserApi/GetAll")]
        [HttpGet]
        public string Get(int page, int pageSize)
        {
            return _userApiService.GetAllUser(page,pageSize);
        }

        [Route("api/UserApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return _userApiService.GetUserById(id);
        }

        [Route("api/UserApi/Add")]
        [HttpPost]
        public Result Post(UserDto newUser)
        {

            return _userApiService.AddUser(newUser);
        }

        [Route("api/UserApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return _userApiService.DeleteUser(id);
        }


        [Route("api/UserApi/Update")]
        [HttpPut]
        public Result Update(int id, UserDto userDto)
        {

            return _userApiService.UpdateUser(id, userDto);
        }


        [Route("api/UserApi/SendMail")]
        [HttpPost]
        public Result SendMail(string mail)
        {
            return _userApiService.SendMailPassword(mail);
        }

        [Route("api/UserApi/UploadProfilePicture")]
        [HttpPost]
        public Result UploadProfilePicture(int id)
        {
            // isteği al.
            var httpRequest = HttpContext.Current.Request;

            // istek içine bak dosya var mı ?
            if (httpRequest.Files.Count > 0)
            {
                // Serverda dosyaları saklayacağım dizini belirt
                string path = HttpContext.Current.Server.MapPath("~/Uploads/UserProfile/");

                // eğer dizin yoksa oluştur
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // istek içinde bulunan dosyaları al
                foreach (string file in httpRequest.Files)
                {
                    //dosyayı değişkende tut
                    var postedFile = httpRequest.Files[file];

                    //dosyaya random isim hazırla.
                    string fNAme = Guid.NewGuid().ToString();  

                    // dosyanın uzantısını al
                    string fExt = Path.GetExtension(postedFile.FileName);

                    // oluşturulan path içinde verdiğin isimle dosyayı yerleştir.Dosya yolunu değişkende tut
                    var filePath = Path.Combine(path, fNAme + fExt);
                 
                    //dosyayı servera kaydet.
                    postedFile.SaveAs(filePath);

                    //ilgili kullanıcının id ' si ile dosyanın adını servise gönder dbye kaydetmesi için.
                   return  _userApiService.UploadProfilePicture(id, fNAme + fExt);
                }

            }
            return Result.Instance.Warning("HATA! Yüklemek istediğiniz fotoğraf yüklenemedi.");

        }
    }
}
