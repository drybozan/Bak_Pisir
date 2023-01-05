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
            CreateMap<SubTransitionTBL, SubTransitionDto>()
                .ForMember(d => d.categoryTransitionId, o => o.MapFrom(s => s.categoryTransitionId))
                .ForMember(d => d.subCategoryId, o => o.MapFrom(s => s.subCategoryId))
                .ForMember(d => d.recipeId, o => o.MapFrom(s => s.recipeId))
                .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))

                .ReverseMap()
                .ForMember(s => s.categoryTransitionId, o => o.MapFrom(d => d.categoryTransitionId))
                .ForMember(s => s.subCategoryId, o => o.MapFrom(d => d.subCategoryId))
                .ForMember(s => s.recipeId, o => o.MapFrom(d => d.recipeId))
                .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))

            ;
        }
    }
}
