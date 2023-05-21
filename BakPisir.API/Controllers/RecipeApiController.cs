using BakPisir.API.Services;
using BakPisir.CORE.Result;
using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace BakPisir.API.Controllers
{
  
    public class RecipeApiController : ApiController
    {

        private RecipeApiService recipeApiService = new RecipeApiService();

        [Route("api/RecipeApi/GetAll")]
        [HttpGet]
        public string Get(int page, int pageSize)
        {
            return recipeApiService.GetAllRecipe( page, pageSize);
        }

        [Route("api/RecipeApi/GetRecipeByCategoryId")]
        [HttpGet]
        public string GetRecipeByCategoryId(int id,int page, int pageSize)
        {
            return recipeApiService.GetRecipeByCategoryId(id,page, pageSize);
        }


        [Route("api/RecipeApi/GetRecipeByUserId")]
        [HttpGet]
        public string GetRecipeByUserId(int id, int page, int pageSize)
        {
            return recipeApiService.GetRecipeByUserId(id, page, pageSize);
        }

        [Route("api/RecipeApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return recipeApiService.GetRecipeById(id);
        }


        [Route("api/RecipeApi/GetRecipeByRecipeName")]
        [HttpGet]
        public string GetRecipeByRecipeName(string recipeName)
        {
            return recipeApiService.GetRecipeByRecipeName(recipeName);
        }

        [Authorize]
        [Route("api/RecipeApi/Add")]
        [HttpPost]
        public Result Post(RecipeDto newRecipe)
        {

           return recipeApiService.AddRecipe(newRecipe);
        }

        [Authorize]
        [Route("api/RecipeApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return recipeApiService.DeleteRecipe(id);
        }

        [Authorize]
        [Route("api/RecipeApi/Update")]
        [HttpPut]
        public Result Update(int id, RecipeDto recipeDto)
        {

            return recipeApiService.UpdateRecipe(id, recipeDto);
        }


        public static string DecodeEncodedFileName(string encodedFileName)
        {
            var regex = new Regex(@"=\?utf-8\?B\?(.*?)\?=");
            var match = regex.Match(encodedFileName);

            if (match.Success)
            {
                var base64String = match.Groups[1].Value;
                var bytes = Convert.FromBase64String(base64String);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            else
            {
                return encodedFileName;
            }
        }

        [Authorize]
        [Route("api/RecipeApi/UploadRecipeVideo")]
        [HttpPost]
        public Result UploadRecipeVideo(int id)
        {
            // isteği al.
            var httpRequest = HttpContext.Current.Request;

            // istek içine bak dosya var mı ?
            if (httpRequest.Files.Count > 0)
            {
                // Serverda dosyaları saklayacağım dizini belirt
                string path = HttpContext.Current.Server.MapPath("~/Uploads/RecipeVideos");

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
                    string decodedString = DecodeEncodedFileName(postedFile.FileName);

                    //dosyaya random isim hazırla.
                    string fNAme = Guid.NewGuid().ToString();

                    // dosyanın uzantısını al
                    string fExt = Path.GetExtension(decodedString);

                    // oluşturulan path içinde verdiğin isimle dosyayı yerleştir.Dosya yolunu değişkende tut
                    var filePath = Path.Combine(path, fNAme + fExt);

                    //dosyayı servera kaydet.
                    postedFile.SaveAs(filePath);

                    //ilgili kullanıcının id ' si ile dosyanın adını servise gönder dbye kaydetmesi için.
                    return recipeApiService.UploadRecipeVideo(id, fNAme + fExt);
                }
            }
            return Result.Instance.Warning("HATA! Yüklemek istediğiniz video yüklenemedi.");
        }

        [Authorize]
        [Route("api/RecipeApi/UploadRecipePicture")]
        [HttpPost]
        public Result UploadRecipePicture(int id)
        {
            // isteği al.
            var httpRequest = HttpContext.Current.Request;

            // istek içine bak dosya var mı ?
            if (httpRequest.Files.Count > 0)
            {
                // Serverda dosyaları saklayacağım dizini belirt
                string path = HttpContext.Current.Server.MapPath("~/Uploads/RecipePictures");

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
                    return recipeApiService.UploadRecipePicture(id, fNAme + fExt);

                }
            }
            return Result.Instance.Warning("HATA! Yüklemek istediğiniz fotoğraf yüklenemedi.");
        }

    }
}
