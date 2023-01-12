using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class SubCategoryService
    {

        WebApiService<SubCategoryDto> was = new WebApiService<SubCategoryDto>();

        //Bütün subcategory tablosunu çeker.
        public List<SubCategoryDto> GetAllSubCategories()
        {
            return was.GetAll("SubCategoryApi/GetAll");
        }

        //Verilen id değerine sahip subcategory verisini çeker.
        public SubCategoryDto GetSingleSubCategory(int id)
        {
            var category = was.GetId("SubCategoryApi/Get", id);
            return category;
        }

        //Verilen id değerine sahip subcategory verilerini çeker.
        public List<SubCategoryDto> GetSubCategoryByCategoryId(int id)
        {
            var categories = was.GetSpecial("SubCategoryApi/GetByCategoryId", id);
            return categories;
        }

        //Verilen id değerine sahip subcategory verisini veritabanından siler.
        public String DeleteSubCategory(int id)
        {
            return was.Delete("SubCategoryApi/Delete", id);
        }

        //Yeni subcategory ekler.
        public String AddSubCategory(SubCategoryDto value)
        {
            return was.Add("SubCategoryApi/Add", value);
        }

        //Verilen id değerine sahip subcategory verisini günceller.
        public String UpdateSubCategory(int id, SubCategoryDto value)
        {
            return was.Update("SubCategoryApi/Update", id, value);
        }
    }
}