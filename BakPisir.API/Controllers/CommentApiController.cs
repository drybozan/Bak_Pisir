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
    [Authorize]
    public class CommentApiController : ApiController
    {
        private CommentApiService commentApiService = new CommentApiService();

        [Route("api/CommentApi/GetAll")]
        [HttpGet]
        public string Get()
        {
            return commentApiService.GetAllComment();
        }

        [Route("api/CommentApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return commentApiService.GetCommentById(id);
        }

        [Route("api/CommentApi/Add")]
        [HttpPost]
        public Result Post(CommentDto newComment)
        {

            return commentApiService.AddComment(newComment);
        }

        [Route("api/CommentApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return commentApiService.DeleteComment(id);
        }


        [Route("api/CommentApi/Update")]
        [HttpPut]
        public Result Update(int id, CommentDto commentDto)
        {

            return commentApiService.UpdateComment(id, commentDto);
        }

        [Route("api/CommentApi/UploadCommentPicture")]
        [HttpPost]
        public Result UploadCommentPicture(int id)
        {
            // isteği al.
            var httpRequest = HttpContext.Current.Request;

            // istek içine bak dosya var mı ?
            if (httpRequest.Files.Count > 0)
            {
                // Serverda dosyaları saklayacağım dizini belirt
                string path = HttpContext.Current.Server.MapPath("~/Uploads/CommentPictures");

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
                   return  commentApiService.UploadCommentPicture(id, fNAme + fExt);
                }
            }
            return Result.Instance.Warning("HATA! Yüklemek istediğiniz fotoğraf yüklenemedi.");
        }
    }
        
}

