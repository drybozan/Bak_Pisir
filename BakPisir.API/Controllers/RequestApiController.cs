using BakPisir.API.Services;
using BakPisir.CORE.Result;
using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakPisir.API.Controllers
{
  
    public class RequestApiController : ApiController
    {
         RequestApiService  requestApiService = new RequestApiService();

        [Authorize]
        [Route("api/RequestApi/GetAll")]
        [HttpGet]
        public string GetAll()
        {
           return requestApiService.GetAllRequest();
        }

        [Authorize]
        [Route("api/RequestApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return requestApiService.GetRequestById(id);
        }

        [Route("api/RequestApi/Add")]
        [HttpPost]
        public Result Post(RequestDto newRequest)
        {
            
           return requestApiService.AddRequest(newRequest);
        }

        [Authorize]
        [Route("api/RequestApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return requestApiService.DeleteRequest(id);
        }

        [Authorize]
        [Route("api/RequestApi/Update")]
        [HttpPut]
        public Result Update(int id, RequestDto requestDto)
        {

            return requestApiService.UpdateRequest(id, requestDto);
        }

        [Authorize]
        [Route("api/RequestApi/SendMailPassword")]
        [HttpPost]
        public Result SendMailPassword(string mail)
        {
            return requestApiService.SendMailPassword(mail);
        }

        [Authorize]
        //Üyelik reddedildiğine dair mail yollar.
        [Route("api/RequestApi/SendMailRejection")]
        [HttpPost]
        public Result SendMailRejection( string mail)
        {
            return requestApiService.SendMailRejection(mail);
        }

        [Authorize]
        //Üyelik başvurusunun alındığına dair mail yollar.
        [Route("api/RequestApi/SendMailInfo")]
        [HttpPost]
        public Result SendMailApp(string mail)
        {
            return requestApiService.SendMailInfo(mail);
        }
    }
}
