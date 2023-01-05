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
    public class LogApiService
    {
        private static BakPisirDBEntities bakPisirDBEntities = new BakPisirDBEntities();

        // unitofwork nesnemi oluşturuyorum ve db context dosyamı parametre veriyorum.
        private EFUnitOfWork efUnitOfWork = new EFUnitOfWork(bakPisirDBEntities);

        public string GetAllLog(int page, int pageSize)
        {
            //projecTo, autommaper aracı. DB varlığımı dto ya mapler.
           
            var logs = efUnitOfWork.LogTemplate.GetAll()
            .OrderBy(o => o.logId) // gelen datayı id ye göre sırala
            .Skip((page - 1) * pageSize) //Sayfa numrası * sayfa boytuna göre belrli bir kayıt kümesini atlar.
               .Take(pageSize)  //Yalnızca sayfa boyutuna göre belirlenen gerekli miktarda veriyi alır.
            .ProjectTo<LogDto>()
               .ToList()
               .ToPaginate(page, pageSize);


            LogListModel mappedLogListModel = logs.MapTo<LogListModel>();
            return JsonConvert.SerializeObject(mappedLogListModel);
        }

        //Verilen id değerine sahip logu veritabanında bulur ve döndürür.
        public string GetLogById(int id)
        {
            var logs = efUnitOfWork.LogTemplate.GetById(id).MapTo<LogDto>();
            return JsonConvert.SerializeObject(logs);
        }

        //Verilen id değerine sahip logu veritabanından siler.
        public Result DeleteLog(int id)
        {
            var result = Result.Instance.Warning("HATA! Böyle bir log yok.");
            var log = efUnitOfWork.LogTemplate.GetById(id);
            if (log != null)
            {
                efUnitOfWork.LogTemplate.Delete(log);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Log başarıyla silindi.");
                return result;
            }

            return result;
        }

        //Yeni log ekler.
        public Result AddLog(LogDto logDto)
        {
            var result = Result.Instance.Warning("HATA! Girdiğiniz bilgileri kontrol ediniz.");

            if (logDto != null) //girilen bilgilerin kontrolü yapılır.
            {
                var mappedLog = logDto.MapTo<LogTBL>();
                efUnitOfWork.LogTemplate.Add(mappedLog);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Log kaydınız başarıyla yapıldı.");

                return result;
            }
            return result;
        }

        //Verilen id değerine sahip logu günceller.
        public Result UpdateLog(int id, LogDto logDto)
        {
            var result = Result.Instance.Warning("HATA! Güncelleme istediğiniz log bulunamadı.");

            // istenilen id mevcutsa güncellenecek data lgoa atanır.
            //GetById Tbl türünde veri getirip mapto yla dto ya çevirdi.
            var log = efUnitOfWork.LogTemplate.GetById(id).MapTo<LogDto>();
            if (log != null)
            {
                var mapped = logDto.MapTo<LogTBL>();
                mapped.logId = log.logId;
                efUnitOfWork.LogTemplate.Update(mapped);
                efUnitOfWork.SaveChanges();

                result = Result.Instance.Success("Log başarıyla güncellendi.");
                return result;
            }
            return result;
        }
    }
}