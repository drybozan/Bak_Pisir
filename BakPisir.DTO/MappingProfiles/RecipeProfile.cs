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
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler.
            CreateMap<RecipeTBL, RecipeDto>()
                .ForMember(d => d.recipeId, o => o.MapFrom(s => s.recipeId))
                .ForMember(d => d.userId, o => o.MapFrom(s => s.userId))
                .ForMember(d => d.categoryId, o => o.MapFrom(s => s.categoryId))
                .ForMember(d => d.cookingTime, o => o.MapFrom(s => s.cookingTime))
                .ForMember(d => d.recipeVideoUrl, o => o.MapFrom(s => s.recipeVideoUrl))
                .ForMember(d => d.ingredients, o => o.MapFrom(s => s.ingredients))
                .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))
                .ForMember(d => d.recipeDate, o => o.MapFrom(s => s.recipeDate))
                .ForMember(d => d.recipeName, o => o.MapFrom(s => s.recipeName))
                .ForMember(d => d.recipeImageUrl, o => o.MapFrom(s => s.recipeImageUrl))
                .ReverseMap()
                .ForMember(s => s.recipeId, o => o.MapFrom(d => d.recipeId))
                .ForMember(s => s.userId, o => o.MapFrom(d => d.userId))
                .ForMember(s => s.categoryId, o => o.MapFrom(d => d.categoryId))
                .ForMember(s => s.cookingTime, o => o.MapFrom(d => d.cookingTime))
                .ForMember(s => s.recipeVideoUrl, o => o.MapFrom(d => d.recipeVideoUrl))
                .ForMember(s => s.ingredients, o => o.MapFrom(d => d.ingredients))
                .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
                .ForMember(s => s.recipeDate, o => o.MapFrom(d => d.recipeDate))
                .ForMember(s => s.recipeName, o => o.MapFrom(d => d.recipeName))
                .ForMember(s => s.recipeImageUrl, o => o.MapFrom(d => d.recipeImageUrl))
                ;
            ;
        }
    }
}
