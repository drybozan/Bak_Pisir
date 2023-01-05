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