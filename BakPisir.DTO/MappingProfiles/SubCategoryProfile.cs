using AutoMapper;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;

public class SubCategoryProfile : Profile
{
    public SubCategoryProfile()
    {
        //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler.
        CreateMap<SubCategoryTBL, SubCategoryDto>()
            .ForMember(d => d.subCategoryId, o => o.MapFrom(s => s.subCategoryId))
            .ForMember(d => d.categoryId, o => o.MapFrom(s => s.categoryId))
            .ForMember(d => d.subCategoryName, o => o.MapFrom(s => s.subCategoryName))           
            .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))          
            .ReverseMap()
            .ForMember(s => s.subCategoryId, o => o.MapFrom(d => d.subCategoryId))
            .ForMember(s => s.categoryId, o => o.MapFrom(d => d.categoryId))
            .ForMember(s => s.subCategoryName, o => o.MapFrom(d => d.subCategoryName))           
            .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
            ;
        ;
    }
}
