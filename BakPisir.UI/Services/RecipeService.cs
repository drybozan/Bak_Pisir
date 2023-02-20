using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class RecipeService
    {
        WebApiService<RecipeDto> was = new WebApiService<RecipeDto>();

        //Recipe tablosunu paging yaparak çeker.
        public List<RecipeDto> GetAllRecipes(int page, int pageSize)
        {
            var recipes = was.Get("RecipeApi/GetAll", page, pageSize);
            return recipes;
        }

        //Verilen id değerine sahip recipe verisini çeker.
        public RecipeDto GetSingleRecipe(int id)
        {
            var recipe = was.GetId("RecipeApi/Get", id);
            return recipe;
        }

        //Verilen id değerine sahip recipe verisini veritabanından siler.
        public String DeleteRecipe(int id)
        {
            return was.Delete("RecipeApi/Delete", id);
        }

        //Yeni recipe ekler.
        public String AddRecipe(RecipeDto value)
        {
            return was.Add("RecipeApi/Add", value);
        }

        //Verilen id değerine sahip recipe verisini günceller.
        public String UpdateRecipe(int id, RecipeDto value)
        {
            return was.Update("RecipeApi/Update", id, value);
        }

        public String UploadRecipeVideo(string method, int id, HttpPostedFileBase file)
        {
            return was.UploadFile("RecipeApi/UploadRecipeVideo", id, file);
        }

        public String UploadRecipePicture(string method, int id, HttpPostedFileBase file)
        {
            return was.UploadFile("RecipeApi/UploadRecipePicture", id, file);
        }
    }
}