using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class CategoryService
    {
        WebApiService<CategoryDto> was = new WebApiService<CategoryDto>();

        //Bütün category tablosunu çeker.
        public List<CategoryDto> GetAllCategories()
        {
            return was.GetAll("CategoryApi/GetAll");
        }

        //Verilen id değerine sahip category verisini çeker.
        public CategoryDto GetSingleCategory(int id)
        {
            var category = was.GetId("CategoryApi/Get", id);
            return category;
        }

        //Verilen id değerine sahip category verisini veritabanından siler.
        public String DeleteCategory(int id)
        {
            return was.Delete("CategoryApi/Delete", id);
        }

        //Yeni category ekler.
        public String AddCategory(CategoryDto value)
        {
            return was.Add("CategoryApi/Add", value);
        }

        //Verilen id değerine sahip category verisini günceller.
        public String UpdateCategory(int id, CategoryDto value)
        {
            return was.Update("CategoryApi/Update", id, value);
        }

    }
}