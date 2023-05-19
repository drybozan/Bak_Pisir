using AutoMapper.QueryableExtensions;
using BakPisir.CORE.Helper;
using BakPisir.CORE.Paging;
using BakPisir.CORE.Result;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.DTO.ModelforList;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace BakPisir.API.Services
{
    public class SubTransitionApiService
    {
        // Önce Database Context dosyamın nesnesini oluşturuyorum.
        private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

        // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
        private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

        public string GetAllSubTransition()
        {
            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
            var subTransitions = efUnitOfWork.SubTransitionTemplate.GetAll(i => i.isDelete == false).ProjectTo<SubTransitionDto>().ToList();
            return JsonConvert.SerializeObject(subTransitions);
        }

        //Verilen id değerine sahip datayı veritabanında bulur ve döndürür.
        public string GetSubTransitionById(int id)
        {
            var subTransition = efUnitOfWork.SubTransitionTemplate.GetById(id).MapTo<SubTransitionDto>();
            return JsonConvert.SerializeObject(subTransition);
        }

        //Verilen id değerine sahip datayı veritabanında bulur ve döndürür.
        public string GetSubTransitionBySubCategoryId(int id , int page, int pageSize)
        {
            var subTransition = efUnitOfWork.SubTransitionTemplate.GetAll(i =>i.subCategoryId==id).ProjectTo<SubTransitionDto>().ToList();

            List<RecipeDto> recipes = new List<RecipeDto>();           
            for (int i = 0; i < subTransition.Count; i++)
            {
                
                var sorgu = efUnitOfWork.RecipeTemplate.GetById(subTransition[i].recipeId).MapTo<RecipeDto>();
                  if(sorgu.isDelete==false)
                    recipes.Add(sorgu);

            }
            recipes.OrderBy(o => o.recipeId); // gelen datayı id ye göre sırala)
           var paginatedRecipes= recipes.ToPaginate(page, pageSize);

            RecipeListModel mappedRecipeListModel = paginatedRecipes.MapTo<RecipeListModel>();
            return JsonConvert.SerializeObject(mappedRecipeListModel); 

           // return JsonConvert.SerializeObject(subTransition);
           
        }


        //recipeId ye göre subcategory değerini döndürür
        //Mapto kullanırken koşullu arama yapabildik.
        public string GetSubCategoryByRecipeId(int recipeId)
        {
            //gönderilen categoryId parametresi ile koşşulu sorgu atar.
            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.

            var subTransitions = efUnitOfWork.SubTransitionTemplate.Get(i => i.recipeId == recipeId);
            var subTransitionDto = subTransitions.MapTo<SubTransitionDto>();

            return JsonConvert.SerializeObject(subTransitionDto);
        }

        //Verilen id değerine sahip datayı veritabanından siler.
        public Result DeleteSubTransition(int id)
        {
            var result = Result.Instance.Warning("HATA! Böyle bir kayıt yok.");
            var subTransition = efUnitOfWork.SubTransitionTemplate.GetById(id);
            if (subTransition != null)
            {
                efUnitOfWork.SubTransitionTemplate.Delete(subTransition);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla silindi.");
                return result;
            }

            return result;
        }

        //Yeni data ekler.
        public Result AddSubTransition(SubTransitionDto subTransitionDto)
        {
            var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

            if (subTransitionDto != null) //girilen bilgilerin kontrolü yapılır.
            {
                var mappedSubTransition = subTransitionDto.MapTo<SubTransitionTBL>();
                efUnitOfWork.SubTransitionTemplate.Add(mappedSubTransition);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla alındı.");

                return result;
            }
            return result;
        }

        //Verilen id değerine sahip başvuru verisini günceller.
        public Result UpdateSubTransition(int id, SubTransitionDto subTransitionDto)
        {
            var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

            // istenilen id mevcutsa güncellenecek data SubTransitione atanır.
            var subTransition = efUnitOfWork.SubTransitionTemplate.GetById(id).MapTo<SubTransitionDto>();
            if (subTransition != null)
            {
                var mapped = subTransitionDto.MapTo<SubTransitionTBL>();
                mapped.categoryTransitionId = subTransition.categoryTransitionId;

                efUnitOfWork.SubTransitionTemplate.Update(mapped);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Kayıt talebi başarıyla güncellendi.");
                return result;
            }
            return result;
        }

    }
}