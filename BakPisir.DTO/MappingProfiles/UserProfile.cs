using AutoMapper;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;

public class UserProfile : Profile
{
    public UserProfile()
    {
        //Dto içerisindeki alanları veritabanı  varlıkları alanları  ile eşler.
        CreateMap<UserTBL, UserDto>()
                .ForMember(d => d.userId, o => o.MapFrom(s => s.userId))
                .ForMember(d => d.name, o => o.MapFrom(s => s.name))
                .ForMember(d => d.surname, o => o.MapFrom(s => s.surname))
                .ForMember(d => d.mail, o => o.MapFrom(s => s.mail))
                .ForMember(d => d.registrationDate, o => o.MapFrom(s => s.registrationDate))
                .ForMember(d => d.phone, o => o.MapFrom(s => s.phone))
                .ForMember(d => d.username, o => o.MapFrom(s => s.username))
                .ForMember(d => d.userType, o => o.MapFrom(s => s.userType))
                .ForMember(d => d.password, o => o.MapFrom(s => s.password))
                .ForMember(d => d.profilePictureUrl, o => o.MapFrom(s => s.profilePictureUrl))
                .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))
                .ForMember(d => d.birthdate, o => o.MapFrom(s => s.birthdate))
                .ForMember(d => d.identityNumber, o => o.MapFrom(s => s.identityNumber))
                .ReverseMap()
                .ForMember(s => s.userId, o => o.MapFrom(d => d.userId))
                .ForMember(s => s.name, o => o.MapFrom(d => d.name))
                .ForMember(s => s.surname, o => o.MapFrom(d => d.surname))
                .ForMember(s => s.mail, o => o.MapFrom(d => d.mail))
                .ForMember(s => s.registrationDate, o => o.MapFrom(d => d.registrationDate))
                .ForMember(s => s.phone, o => o.MapFrom(d => d.phone))
                .ForMember(s => s.username, o => o.MapFrom(d => d.username))
                .ForMember(s => s.userType, o => o.MapFrom(d => d.userType))
                .ForMember(s => s.password, o => o.MapFrom(d => d.password))
                .ForMember(s => s.profilePictureUrl, o => o.MapFrom(d => d.profilePictureUrl))
                .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
                .ForMember(s => s.identityNumber, o => o.MapFrom(d => d.identityNumber))
            ;
    }
}