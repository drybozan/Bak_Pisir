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

    public class SubCategoryApiController : ApiController
    {
        private SubCategoryApiService subCategoryApiService = new SubCategoryApiService();

        [Route("api/SubCategoryApi/GetAll")]
        [HttpGet]
        public string Get()
        {
            return subCategoryApiService.GetAllSubCategory();
        }

        [Route("api/SubCategoryApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return subCategoryApiService.GetSubCategoryById(id);
        }

        [Route("api/SubCategoryApi/GetByCategoryId")]
        [HttpGet]
        public string GetByCategoryId(int id)
        {
            return subCategoryApiService.GetSubCategoryByCategoryId(id);
        }

        [Authorize]
        [Route("api/SubCategoryApi/Add")]
        [HttpPost]
        public Result Post(SubCategoryDto newSubCategory)
        {

           return subCategoryApiService.AddSubCategory(newSubCategory);
        }

        [Authorize]
        [Route("api/SubCategoryApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return subCategoryApiService.DeleteSubCategory(id);
        }

        [Authorize]
        [Route("api/SubCategoryApi/Update")]
        [HttpPut]
        public Result Update(int id, SubCategoryDto subCategoryDto)
        {

            return subCategoryApiService.UpdateSubCategory(id, subCategoryDto);
        }
    }
}
