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
    public class StepApiController : ApiController
    {
        private StepApiService stepApiService = new StepApiService();

        [Route("api/StepApi/GetAll")]
        [HttpGet]
        public string Get()
        {
            return stepApiService.GetAllStep();
        }

        [Route("api/StepApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return stepApiService.GetStepById(id);
        }

        [Route("api/StepApi/Add")]
        [HttpPost]
        public Result Post(StepDto newStep)
        {

            return stepApiService.AddStep(newStep);
        }

        [Route("api/StepApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return stepApiService.DeleteStep(id);
        }


        [Route("api/StepApi/Update")]
        [HttpPut]
        public Result Update(int id, StepDto stepDto)
        {

            return stepApiService.UpdateStep(id, stepDto);
        }


        [Route("api/StepApi/UploadStepPicture")]
        [HttpPost]
        public Result UploadStepPicture(int id)
        {
            // isteği al.
            var httpRequest = HttpContext.Current.Request;

            // istek içine bak dosya var mı ?
            if (httpRequest.Files.Count > 0)
            {
                // Serverda dosyaları saklayacağım dizini belirt
                string path = HttpContext.Current.Server.MapPath("~/Uploads/StepPictures");

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
                   return stepApiService.UploadStepPicture(id, fNAme + fExt);

                }
            }
            return Result.Instance.Warning("HATA! Yüklemek istediğiniz fotoğraf yüklenemedi.");
        }
    
}
}
