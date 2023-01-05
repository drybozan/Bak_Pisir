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
    //[Authorize]
    public class CategoryApiController : ApiController
    {
        private CategoryApiService _categoryApiService = new CategoryApiService();

        [Route("api/CategoryApi/GetAll")]
        [HttpGet]
        public string Get()
        {
            return _categoryApiService.GetAllCategory();
        }

        [Route("api/CategoryApi/Get")]
        [HttpGet]
        public string Get(int id)
        {
            return _categoryApiService.GetCategoryById(id);
        }

        [Route("api/CategoryApi/Add")]
        [HttpPost]
        public Result Post(CategoryDto newCategory)
        {

           return _categoryApiService.AddCategory(newCategory);
        }

        [Route("api/CategoryApi/Delete")]
        [HttpDelete]
        public Result Delete(int id)
        {

            return _categoryApiService.DeleteCategory(id);
        }


        [Route("api/CategoryApi/Update")]
        [HttpPut]
        public Result Update(int id, CategoryDto categoryDto)
        {

            return _categoryApiService.UpdateCategory(id,categoryDto);
        }
    }
}

