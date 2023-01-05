using AutoMapper;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;

public class StepProfile : Profile
{
    public StepProfile()
    {
        //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler tüm alanları.
        //Önce dto varlığının alanlarını tablo alanlarımla eşliyorum
        //sonra tablo alanlarımı dto alanlarım ile eşliyorum.
        CreateMap<StepTBL, StepDto>()
            .ForMember(d => d.stepId, o => o.MapFrom(s => s.stepId))
            .ForMember(d => d.recipeId, o => o.MapFrom(s => s.recipeId))
            .ForMember(d => d.stepName, o => o.MapFrom(s => s.stepName))
            .ForMember(d => d.stepDescription, o => o.MapFrom(s => s.stepDescription))
            .ForMember(d => d.stepImageUrl, o => o.MapFrom(s => s.stepImageUrl))
            .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))
            .ReverseMap()
            .ForMember(s => s.stepId, o => o.MapFrom(d => d.stepId))
            .ForMember(s => s.recipeId, o => o.MapFrom(d => d.recipeId))
            .ForMember(s => s.stepName, o => o.MapFrom(d => d.stepName))
            .ForMember(s => s.stepDescription, o => o.MapFrom(d => d.stepDescription))
            .ForMember(s => s.stepImageUrl, o => o.MapFrom(d => d.stepImageUrl))
            .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
        ;
    }
}