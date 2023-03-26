using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class SubTransitionService
    {
        WebApiService<SubTransitionDto> was = new WebApiService<SubTransitionDto>();
        WebApiService<RecipeListModel> _was = new WebApiService<RecipeListModel>();

        //Bütün SubTransition tablosunu çeker.
        public List<SubTransitionDto> GetAllSubTransition()
        {
            return was.GetAll("SubTransitionApi/GetAll");
        }

        //Verilen id değerine sahip SubTransition verisini çeker.
        public SubTransitionDto GetSingleSubTransition(int id)
        {
            var subTransition = was.GetId("SubTransitionApi/Get", id);
            return subTransition;
        }
        //Verilen subcategoryId değerine sahip tüm tarifleri verilerini çeker.
        public RecipeListModel GetSubTransitionBySubCategoryId(int id, int page, int pageSize)
        {
            var recipes = _was.Get("SubTransitionApi/GetSubTransitionBySubCategoryId", id, page, pageSize);
            return recipes;
        }

        //Verilen id değerine sahip SubTransition verisini veritabanından siler.
        public String DeleteSubTransition(int id)
        {
            return was.Delete("SubTransitionApi/Delete", id);
        }

        //Yeni SubTransition ekler.
        public String AddSubTransition(SubTransitionDto value)
        {
            return was.Add("SubTransitionApi/Add", value);
        }

        //Verilen id değerine sahip SubTransition verisini günceller.
        public String UpdateSubTransition(int id, SubTransitionDto value)
        {
            return was.Update("SubTransitionApi/Update", id, value);
        }
    }
}