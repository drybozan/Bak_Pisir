using BakPisir.DTO.Dtos;
using BakPisir.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BakPisir.UI.Services
{
    public class LogService
    {
        WebApiService<LogDto> was = new WebApiService<LogDto>();

        public List<LogDto> GetAllLogs()
        {
            return was.GetAll("LogApi/GetAll");
        }

        //Verilen id değerine sahip category verisini veritabanından siler.
        public String DeleteLog(int id)
        {
            return was.Delete("LogApi/Delete", id);
        }

        //Verilen id değerine sahip log verisini çeker.
        public LogDto GetSingleLog(int id)
        {
            var log = was.GetId("LogApi/Get", id);
            return log;
        }


        //Yeni log ekler.
        public void AddLog(LogDto value)
        {
            was.Add("LogApi/Add", value);
        }
    }

}

