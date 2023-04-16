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
                .ForPath(d => d.category.categoryName, opt => opt.MapFrom(s => s.CategoryTBL.categoryName))
                .ForPath(d => d.user.name, opt => opt.MapFrom(s => s.UserTBL.name))
                .ForPath(d => d.user.surname, opt => opt.MapFrom(s => s.UserTBL.surname))
                .ForPath(d => d.user.username, opt => opt.MapFrom(s => s.UserTBL.username))
                .ForPath(d => d.user.profilePictureUrl, opt => opt.MapFrom(s => s.UserTBL.profilePictureUrl))
                .ForPath(d => d.user.userId, opt => opt.MapFrom(s => s.UserTBL.userId))
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
                .ForPath(s => s.CategoryTBL.categoryName, opt => opt.Ignore())
                .ForPath(s => s.UserTBL.name, opt => opt.Ignore())
                .ForPath(s => s.UserTBL.surname, opt => opt.Ignore())
                .ForPath(s => s.UserTBL.username, opt => opt.Ignore())
                .ForPath(s => s.UserTBL.profilePictureUrl, opt => opt.Ignore())
                .ForPath(s => s.UserTBL.userId, opt => opt.Ignore())
                ;
            
            ;
        }
    }
}
