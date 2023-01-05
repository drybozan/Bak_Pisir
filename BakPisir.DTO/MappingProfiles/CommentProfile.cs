using AutoMapper;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler tüm alanları.
        //Önce dto varlığının alanlarını tablo alanlarımla eşliyorum
        //sonra tablo alanlarımı dto alanlarım ile eşliyorum.
        CreateMap<CommentTBL, CommentDto>()
            .ForMember(d => d.commentId, o => o.MapFrom(s => s.commentId))
            .ForMember(d => d.recipeId, o => o.MapFrom(s => s.recipeId))
            .ForMember(d => d.userId, o => o.MapFrom(s => s.userId))
            .ForMember(d => d.comment, o => o.MapFrom(s => s.comment))
            .ForMember(d => d.parentId, o => o.MapFrom(s => s.parentId))
            .ForMember(d => d.commentImageUrl, o => o.MapFrom(s => s.commentImageUrl))
            .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))
            .ForMember(d => d.commentDate, o => o.MapFrom(s => s.commentDate))
            .ReverseMap()
            .ForMember(s => s.commentId, o => o.MapFrom(d => d.commentId))
            .ForMember(s => s.recipeId, o => o.MapFrom(d => d.recipeId))
            .ForMember(s => s.userId, o => o.MapFrom(d => d.userId))
            .ForMember(s => s.comment, o => o.MapFrom(d => d.comment))
            .ForMember(s => s.parentId, o => o.MapFrom(d => d.parentId))
            .ForMember(s => s.commentImageUrl, o => o.MapFrom(d => d.commentImageUrl))
            .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
            .ForMember(s => s.commentDate, o => o.MapFrom(d => d.commentDate))


        ;
    }
}