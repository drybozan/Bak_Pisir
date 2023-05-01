using AutoMapper;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakPisir.DTO.MappingProfiles
{
    public class SubTransitionProfile : Profile
    {
        public SubTransitionProfile()
        {
            //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler tüm alanları.
            //Önce dto varlığının alanlarını tablo alanlarımla eşliyorum
            //sonra tablo alanlarımı dto alanlarım ile eşliyorum.
            _ = CreateMap<SubTransitionTBL, SubTransitionDto>()
                .ForMember(d => d.categoryTransitionId, o => o.MapFrom(s => s.categoryTransitionId))
                .ForMember(d => d.subCategoryId, o => o.MapFrom(s => s.subCategoryId))
                .ForMember(d => d.recipeId, o => o.MapFrom(s => s.recipeId))
                .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))
                .ForPath(d => d.subCategory.subCategoryId, opt => opt.MapFrom(s => s.SubCategoryTBL.subCategoryId))
                .ForPath(d => d.subCategory.subCategoryName, opt => opt.MapFrom(s => s.SubCategoryTBL.subCategoryName))
                .ForPath(d => d.subCategory.categoryId, opt => opt.MapFrom(s => s.SubCategoryTBL.categoryId))
                .ForPath(d => d.subCategory.recipeId, opt => opt.MapFrom(s => s.SubCategoryTBL.recipeId))
                .ForPath(d => d.subCategory.isDelete, opt => opt.MapFrom(s => s.SubCategoryTBL.isDelete))
                .ReverseMap()
                .ForMember(s => s.categoryTransitionId, o => o.MapFrom(d => d.categoryTransitionId))
                .ForMember(s => s.subCategoryId, o => o.MapFrom(d => d.subCategoryId))
                .ForMember(s => s.recipeId, o => o.MapFrom(d => d.recipeId))
                .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
                .ForPath(s => s.SubCategoryTBL.subCategoryId, opt => opt.Ignore())
                .ForPath(s => s.SubCategoryTBL.subCategoryName, opt => opt.Ignore())
                .ForPath(s => s.SubCategoryTBL.categoryId, opt => opt.Ignore())
                .ForPath(s => s.SubCategoryTBL.recipeId, opt => opt.Ignore())
                .ForPath(s => s.SubCategoryTBL.isDelete, opt => opt.Ignore())

            ;

           
        }
    }
}
