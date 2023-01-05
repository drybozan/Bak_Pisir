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

        [Route("api/RequestApi/GetAll")]
        [HttpGet]
        public string Get(int page , int pageSize)
        {
           return requestApiService.GetAllRequest(page,pageSize);
        }

        [Route("api/RequestApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return requestApiService.GetRequestById(id);
        }

        [Route("api/RequestApi/Add")]
        [HttpPost]
        public void Post(RequestDto newRequest)
        {
            
            requestApiService.AddRequest(newRequest);
        }

        [Route("api/RequestApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return requestApiService.DeleteRequest(id);
        }


        [Route("api/RequestApi/Update")]
        [HttpPut]
        public Result Update(int id, RequestDto requestDto)
        {

            return requestApiService.UpdateRequest(id, requestDto);
        }

        [Route("api/RequestApi/SendMailPassword")]
        [HttpPost]
        public Result SendMailPassword(string name)
        {
            return requestApiService.SendMailPassword(name);
        }

        //Üyelik reddedildiğine dair mail yollar.
        [Route("api/RequestApi/SendMailRejection")]
        [HttpPost]
        public Result SendMailRejection( string name)
        {
            return requestApiService.SendMailRejection(name);
        }

        //Üyelik başvurusunun alındığına dair mail yollar.
        [Route("api/RequestApi/SendMailInfo")]
        [HttpPost]
        public Result SendMailApp(string name)
        {
            return requestApiService.SendMailInfo(name);
        }
    }
}
