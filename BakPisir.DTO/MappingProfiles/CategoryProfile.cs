using AutoMapper;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler.
        CreateMap<CategoryTBL, CategoryDto>()
            .ForMember(d => d.categoryId, o => o.MapFrom(s => s.categoryId))
            .ForMember(d => d.categoryName, o => o.MapFrom(s => s.categoryName))
            .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))
            
            .ReverseMap()
            .ForMember(s => s.categoryId, o => o.MapFrom(d => d.categoryId))
            .ForMember(s => s.categoryName, o => o.MapFrom(d => d.categoryName))
            .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
           
            ;
        ;
    }
}
