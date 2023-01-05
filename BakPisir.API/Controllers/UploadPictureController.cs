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
    public class UploadPictureController : ApiController
    {
        //POST

        [Route("api/uploadpicture/upload")]
        public HttpResponseMessage Upload()
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Uploads/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //dosya uzantılarını tutmak için list
                    var docfiles = new List<string>();

                     // istek içindeki dosyaları okur 
                    foreach (string file in httpRequest.Files)
                    {
                        // her dosyayı alır
                        var postedFile = httpRequest.Files[file];
                        string dir = Path.GetDirectoryName(postedFile.FileName);
                    // dosya ismini al
                        string fNAme = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    //dosya uzantısını al
                        string fExt = Path.GetExtension(postedFile.FileName);
                    //dosyayı belirtilen klasör altına göndermek için isim kombin et
                        var filePath = path + postedFile.FileName;
                        int i = 1;
                    // eğer aynı isimde klaösrleme yapısında varsa i kadar isim ekle
                        while (File.Exists(filePath))
                        {
                            filePath = Path.Combine(path, fNAme + "_" + i + fExt);
                            i++;
                        }
                        // doyasının saklanacağı adresi akydet
                        postedFile.SaveAs(filePath);
                    // isimlendirilen dosyaların uzantıları list e eklenir.
                        docfiles.Add(filePath);
                    }

                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return result;
            }
        }
    }

