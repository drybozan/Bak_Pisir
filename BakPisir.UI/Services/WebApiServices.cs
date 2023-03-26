using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using BakPisir.CORE.Helper;
using System.Web.Services.Description;


namespace BakPisir.UI.Services
{
    public class WebApiService<T> where T : class
    {
        static string url = "http://localhost:44355/api/";

        //Bütün verileri listeleyen generic fonksiyon.
        // GetAll fonk. için
        public List<T> GetAll(string method)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                HttpResponseMessage response = _httpClient.GetAsync(method).Result;
                return ConvertList(response);
            }
        }

    

        //parçalı veri getirir.
        // paging için
        public T Get(string method, int page, int pageSize)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                HttpResponseMessage response = _httpClient.GetAsync(method + "/?page=" + page + "&pageSize=" + pageSize).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                var newData = JsonConvert.DeserializeObject<T>(JsonConvert.DeserializeObject<string>(data));                
                return newData;
            }
        }

        //parçalı veri getirir. Seçilen kategoriye göre
        // paging için
        public T Get(string method,int id, int page, int pageSize)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                HttpResponseMessage response = _httpClient.GetAsync(method +"/?id="+id + "&page=" + page + "&pageSize=" + pageSize).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                var newData = JsonConvert.DeserializeObject<T>(JsonConvert.DeserializeObject<string>(data));
                return newData;
            }
        }

        //Verilen id değerine sahip veriyi getirir.
        // GetBy Id 
        public T GetId(string method, int id)
        {
            using (HttpClient _httpClient= new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
               _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                   _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                var response = _httpClient.GetAsync(method+ "?id=" + id).Result;
                var message = response.Content.ReadAsStringAsync().Result;
                var newData = JsonConvert.DeserializeObject<T>(JsonConvert.DeserializeObject<string>(message));
                return newData;
            }
        }


        //Fverilen parametreye göre koşullu sorgu sonuçlarını getirir
        public List<T> GetSpecial(string method, int id)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                HttpResponseMessage response = _httpClient.GetAsync(method + "?id=" + id).Result;
                return ConvertList(response);
            }

        }

        //Gelen yeni veriyi veritabanına ekler.
        // post metodları için
        public String Add(string method, T value)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(method, httpContent).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                dynamic newData = JsonConvert.DeserializeObject(data);
                var message = newData.Message;
                return message;
            }

        }


        //Verilen mail adresini yollar.
        public string GetMailFeedback(string method, string mail)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(mail), Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(method+"?mail="+mail, httpContent).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                dynamic newData = JsonConvert.DeserializeObject(data);
                var message= newData.Message;                
                return message;
            }
        }

        //Verilen id değerine sahip veriyi günceller.
        public String Update(string method, int id, T value)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync(method + "?id=" + id, httpContent).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                dynamic newData = JsonConvert.DeserializeObject(data);
                var message = newData.Message;
                return message;
            }
        }

        //Verilen id değerine sahip veriyi siler.
        public String Delete(string method, int id)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                HttpResponseMessage response = _httpClient.DeleteAsync(method + "?id=" + id).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                dynamic newData = JsonConvert.DeserializeObject(data);
                var message = newData.Message;
                return message;
            }

        }

        //Webapiden gelen veriyi listeye çevirir
        public List<T> ConvertList(HttpResponseMessage response)
        {
            List<T> newData = new List<T>();
            var message = response.Content.ReadAsStringAsync().Result;
            newData = JsonConvert.DeserializeObject<List<T>>(JsonConvert.DeserializeObject<string>(message));
            return newData;
        }

        //Gönderilen veriler ile token alınmasını sağlar.
        public T GetToken(string username, string password)
        {
            string baseAddress = "http://localhost:44355/";
            T token;
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username", username},
                    {"password", password},
                };
                var tokenResponse = client.PostAsync(baseAddress + "token", new FormUrlEncodedContent(form)).Result;
                var tok = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<T>(tok);
            }
            return token;
        }

        //Verilen id'ye gelen dosyaları yükler.
        public String UploadFile(string method, int id, HttpPostedFileBase file)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] Bytes = target.ToArray();
                    file.InputStream.Read(Bytes, 0, Bytes.Length);
                    var fileContent = new ByteArrayContent(Bytes);
                    fileContent.Headers.ContentDisposition =
                        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                    content.Add(fileContent);
                    content.Add(new StringContent("123"), "FileId");

                    _httpClient.BaseAddress = new Uri(url);
                    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                    HttpResponseMessage response = _httpClient.PostAsync(method + "?id=" + id, content).Result;

                    var data = response.Content.ReadAsStringAsync().Result;
                    dynamic newData = JsonConvert.DeserializeObject(data);
                    var message = newData.Message;
                    return message;
                }
            }
        }

        //Token üzerindeki kimlik bilgilerinin alınmasını sağlar.
        
        public T GetSingle(string method)
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(url);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + SessionHelper.TokenInfo.access_token);
                HttpResponseMessage response = _httpClient.GetAsync(method).Result;
                var message = response.Content.ReadAsStringAsync().Result;
                var newData = JsonConvert.DeserializeObject<T>(JsonConvert.DeserializeObject<string>(message));
                return newData;
            }
        }
    }
}