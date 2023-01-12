using AutoMapper.QueryableExtensions;
using BakPisir.CORE.Helper;
using BakPisir.CORE.Result;
using BakPisir.CORE.UnitOfWork;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.API.Services
{
    public class SubCategoryApiService
    {
        // Önce Database Context dosyamın nesnesini oluşturuyorum.
        private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

        // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
        private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

        public string GetAllSubCategory()
        {
            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
            var subCategories = efUnitOfWork.SubCategoryTemplate.GetAll(i => i.isDelete == false).ProjectTo<SubCategoryDto>().ToList();
            return JsonConvert.SerializeObject(subCategories);
        }

        //Verilen id değerine sahip alt kategoriyi veritabanında bulur ve döndürür.
        public string GetSubCategoryById(int id)
        {
            var subCategory = efUnitOfWork.SubCategoryTemplate.GetById(id).MapTo<SubCategoryDto>();
            return JsonConvert.SerializeObject(subCategory);
        }

        public string GetSubCategoryByCategoryId(int categoryId)
        {
            //gönderilen categoryId parametresi ile koşşulu sorgu atar.
            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
            var subCategories = efUnitOfWork.SubCategoryTemplate.GetAll(i => i.categoryId == categoryId).ProjectTo<SubCategoryDto>().ToList();
            return JsonConvert.SerializeObject(subCategories);
        }

        //Verilen id değerine sahip veriyi veritabanından siler.
        public Result DeleteSubCategory(int id)
        {
            var result = Result.Instance.Warning("HATA! Böyle bir kayıt yok.");
            var subCategory = efUnitOfWork.SubCategoryTemplate.GetById(id);
            if (subCategory != null)
            {
                efUnitOfWork.SubCategoryTemplate.Delete(subCategory);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla silindi.");
                return result;
            }

            return result;
        }

        //Yeni alt kategori ekler.
        public Result AddSubCategory(SubCategoryDto subCategoryDto)
        {
            var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

            if (subCategoryDto != null) //girilen bilgilerin kontrolü yapılır.
            {
                var mappedSubCategory = subCategoryDto.MapTo<SubCategoryTBL>();
                efUnitOfWork.SubCategoryTemplate.Add(mappedSubCategory);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Alt kategori başarıyla eklendi.");

                return result;
            }
            return result;
        }

        //Verilen id değerine sahip alt kategoriyi günceller.
        public Result UpdateSubCategory(int id, SubCategoryDto subCategoryDto)
        {
            var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

            // istenilen id mevcutsa güncellenecek data SubCategorye atanır.
            var subCategory = efUnitOfWork.SubCategoryTemplate.GetById(id).MapTo<SubCategoryDto>();
            if (subCategory != null)
            {
                var mapped = subCategoryDto.MapTo<SubCategoryTBL>();
                mapped.subCategoryId = subCategory.subCategoryId;

                efUnitOfWork.SubCategoryTemplate.Update(mapped);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Alt kategori başarıyla güncellendi.");
                return result;
            }
            return result;
        }

    }
}
