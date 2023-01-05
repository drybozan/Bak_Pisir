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
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler tüm alanları.
            //Önce dto varlığının alanlarını tablo alanlarımla eşliyorum
            //sonra tablo alanlarımı dto alanlarım ile eşliyorum.
            CreateMap<RequestTBL, RequestDto>()
                .ForMember(d => d.requestId, o => o.MapFrom(s => s.requestId))
                .ForMember(d => d.name, o => o.MapFrom(s => s.name))
                .ForMember(d => d.surname, o => o.MapFrom(s => s.surname))
                .ForMember(d => d.mail, o => o.MapFrom(s => s.mail))
                .ForMember(d => d.requestDate, o => o.MapFrom(s => s.requestDate))
                .ForMember(d => d.phone, o => o.MapFrom(s => s.phone))
                .ForMember(d => d.username, o => o.MapFrom(s => s.username))
                .ForMember(d => d.isDelete, o => o.MapFrom(s => s.isDelete))
                .ForMember(d => d.birthdate, o => o.MapFrom(s => s.birthdate))
                .ForMember(d => d.agreement, o => o.MapFrom(s => s.agreement))
                .ForMember(d => d.identityNumber, o => o.MapFrom(s => s.identityNumber))
                .ForMember(d => d.password, o => o.MapFrom(s => s.password))
                .ReverseMap()
                .ForMember(s => s.requestId, o => o.MapFrom(d => d.requestId))
                .ForMember(s => s.name, o => o.MapFrom(d => d.name))
                .ForMember(s => s.surname, o => o.MapFrom(d => d.surname))
                .ForMember(s => s.mail, o => o.MapFrom(d => d.mail))
                .ForMember(s => s.requestDate, o => o.MapFrom(d => d.requestDate))
                .ForMember(s => s.phone, o => o.MapFrom(d => d.phone))
                .ForMember(s => s.username, o => o.MapFrom(d => d.username))
                .ForMember(s => s.isDelete, o => o.MapFrom(d => d.isDelete))
                .ForMember(s => s.agreement, o => o.MapFrom(d => d.agreement))
                .ForMember(s => s.identityNumber, o => o.MapFrom(d => d.identityNumber))
                .ForMember(s => s.password, o => o.MapFrom(d => d.password));
        }
    }
}
