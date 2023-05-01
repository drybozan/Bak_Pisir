using BakPisir.DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakPisir.UI.Services
{
    public class StepService
    {
        WebApiService<StepDto> was = new WebApiService<StepDto>();

        //Bütün step tablosunu çeker.
        public List<StepDto> GetAllSteps()
        {
            return was.GetAll("StepApi/GetAll"); 
        }

        //Verilen id değerine sahip step verisini çeker.
        public List<StepDto> GetStepByRecipeId(int id)
        {
            var step = was.GetSpecial("StepApi/GetStepByRecipeId", id);
            return step;
        }

        //Verilen id değerine sahip step verisini çeker.
        public StepDto GetSingleStep(int id)
        {
            var step = was.GetId("StepApi/Get", id);
            return step;
        }

        //Verilen id değerine sahip step verisini veritabanından siler.
        public String DeleteStep(int id)
        {
            return was.Delete("StepApi/Delete", id);
        }

        //Yeni step ekler.
        public String AddStep(StepDto value)
        {
            return was.Add("StepApi/Add", value);
        }

        //Verilen id değerine sahip step verisini günceller.
        public String UpdateStep(int id, StepDto value)
        {
            return was.Update("StepApi/Update", id, value);
        }

        public String UploadStepPicture(int id, HttpPostedFileBase file)
        {
            return was.UploadFile("StepApi/UploadStepPicture", id, file);
        }
    }
}