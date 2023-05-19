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
    public class StepApiService
    {
        // Önce Database Context dosyamın nesnesini oluşturuyorum.
        private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

        // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
        private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

        public string GetAllStep()
        {
            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
            var steps = efUnitOfWork.StepTemplate.GetAll(i => i.isDelete == false).ProjectTo<StepDto>().ToList();
            return JsonConvert.SerializeObject(steps);
        }

        //Verilen id değerine sahip step verisini veritabanında bulur ve döndürür.
        public string GetStepById(int id)
        {
            var step = efUnitOfWork.StepTemplate.GetById(id).MapTo<StepDto>();
            return JsonConvert.SerializeObject(step);
        }

        //Verilen recipeId değerine sahip step verisini veritabanında bulur ve döndürür.
        public string GetStepByRecipeId(int id)
        {
            var steps = efUnitOfWork.StepTemplate.GetAll(i => i.recipeId== id).ProjectTo<StepDto>().ToList();
            return JsonConvert.SerializeObject(steps);
        }

        //Verilen id değerine sahip başvuru verisini veritabanından siler.
        public Result DeleteStep(int id)
        {
            var result = Result.Instance.Warning("HATA! Böyle bir adım kaydı yok.");
            var step = efUnitOfWork.StepTemplate.GetById(id);
            if (step != null)
            {
                efUnitOfWork.StepTemplate.Delete(step);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla silindi.");
                return result;
            }

            return result;
        }

        //Yeni adım ekler.
        public Result AddStep(StepDto stepDto)
        {
            var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

            if (stepDto != null) //girilen bilgilerin kontrolü yapılır.
            {
                var mappedStep = stepDto.MapTo<StepTBL>();
                efUnitOfWork.StepTemplate.Add(mappedStep);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla eklendi.",mappedStep.stepId);

                return result;
            }
            return result;
        }

        //Verilen id değerine sahip adımı günceller.
        public Result UpdateStep(int id, StepDto stepDto)
        {
            var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

            // istenilen id mevcutsa güncellenecek data Stepe atanır.
            var step = efUnitOfWork.StepTemplate.GetById(id).MapTo<StepDto>();
            if (step != null)
            {
                var mapped = stepDto.MapTo<StepTBL>();
                mapped.stepId = step.stepId;

                efUnitOfWork.StepTemplate.Update(mapped);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Başarıyla güncellendi.");
                return result;
            }
            return result;
        }

        public Result UploadStepPicture(int id, string filePath)
        {
            var result = Result.Instance.Warning("HATA! Güncellemek istediğiniz kayıt bulunamadı.");

            // gelen id yi sorgula db de sorgula. 
            var step = efUnitOfWork.StepTemplate.GetById(id).MapTo<StepDto>();

            if (step != null)
            {
                //gönderilen dosyanın yolunu gönder.
                step.stepImageUrl = filePath;

                //veritabanı nesnesi ile maple.
                var mappedStep = step.MapTo<StepTBL>();

                // yapılan değişikliği güncelle
                efUnitOfWork.StepTemplate.Update(mappedStep);

                // değişiklikleri kaydet ve veritabanına yansıt
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Fotoğraf başarıyla eklendi.");
                return result;
            }
            return result;

        }

    }
}