using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class RequestService
    {
        WebApiService<RequestDto> was = new WebApiService<RequestDto>();
        WebApiService<RequestListModel> _was = new WebApiService<RequestListModel>();

        //Bütün request tablosunu çeker.
        public RequestListModel GetAllRequest(int page, int pageSize)
        {
            return _was.Get("RequestApi/GetAll", page, pageSize);
        }

        //Verilen id değerine sahip request verisini çeker.
        public RequestDto GetSingleRequest(int id)
        {
            var request = was.GetId("RequestApi/Get", id);
            return request;
        }

        //Verilen id değerine sahip request verisini veritabanından siler.
        public String DeleteRequest(int id)
        {
            return was.Delete("RequestApi/Delete", id);
        }

        //Yeni request ekler.
        public String AddRequest(RequestDto value)
        {
            return was.Add("RequestApi/Add", value);
        }

        //Verilen id değerine sahip request verisini günceller.
        public String UpdateRequest(int id, RequestDto value)
        {
            return was.Update("RequestApi/Update", id, value);
        }

        //mail ile kayıt isteğinin onaylandığını ve şifre bildirir.
        public string SendMailPassword(string mail)
        {
            return was.GetMailFeedback("RequestApi/SendMail", mail);
        }

        // kayıt isteği red edildiyse mail ile bildirilir
        public string SendMailRejection(string mail)
        {
            return was.GetMailFeedback("RequestApi/SendMailRejection", mail);
        }
        //kayıt isteğinin alındığına dair bilgi gönderir.
        public string SendMailInfo(string mail)
        {
            return was.GetMailFeedback("RequestApi/SendMailInfo", mail);
        }
    }
}