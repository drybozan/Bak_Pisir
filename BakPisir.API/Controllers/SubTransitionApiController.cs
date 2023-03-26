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
   // [Authorize]
    public class SubTransitionApiController : ApiController
    {
        private SubTransitionApiService _subTransitionApiService = new SubTransitionApiService();

        [Route("api/SubTransitionApi/GetAll")]
        [HttpGet]
        public string Get()
        {
            return _subTransitionApiService.GetAllSubTransition();
        }

        [Route("api/SubTransitionApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return _subTransitionApiService.GetSubTransitionById(id);
        }

        [Route("api/SubTransitionApi/GetSubTransitionBySubCategoryId")]
        [HttpGet]
        public string GetBySubCategoryId(int id, int page, int pageSize)
        {
            return _subTransitionApiService.GetSubTransitionBySubCategoryId(id,page,pageSize);
        }

        [Route("api/SubTransitionApi/Add")]
        [HttpPost]
        public Result Post(SubTransitionDto newSubTransition)
        {

            return _subTransitionApiService.AddSubTransition(newSubTransition);
        }

        [Route("api/SubTransitionApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return _subTransitionApiService.DeleteSubTransition(id);
        }


        [Route("api/SubTransitionApi/Update")]
        [HttpPut]
        public Result Update(int id, SubTransitionDto subTransitionDto)
        {

            return _subTransitionApiService.UpdateSubTransition(id, subTransitionDto);
        }
    }
}
