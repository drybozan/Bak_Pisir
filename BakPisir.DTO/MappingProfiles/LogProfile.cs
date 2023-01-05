using AutoMapper;
using BakPisir.DTO.Dtos;
using BakPisir.EF.Models;

public class LogProfile : Profile
{
    public LogProfile()
    {
        //Dto içerisindeki varlıkları veritabanı içerisindeki varlıklar ile eşler.
        CreateMap<LogTBL, LogDto>()
            .ForMember(d => d.logId, o => o.MapFrom(s => s.logId))
            .ForMember(d => d.userId, o => o.MapFrom(s => s.userId))
            .ForMember(d => d.logActivity, o => o.MapFrom(s => s.logActivity))
            .ForMember(d => d.logDate, o => o.MapFrom(s => s.logDate))
            .ForMember(d => d.ipAddress, o => o.MapFrom(s => s.ipAddress))
            .ForMember(d => d.logUsername, o => o.MapFrom(s => s.logUsername))
            .ReverseMap()
            .ForMember(s => s.logId, o => o.MapFrom(d => d.logId))
            .ForMember(s => s.userId, o => o.MapFrom(d => d.userId))
            .ForMember(s => s.logActivity, o => o.MapFrom(d => d.logActivity))
            .ForMember(s => s.logDate, o => o.MapFrom(d => d.logDate))
            .ForMember(s => s.ipAddress, o => o.MapFrom(d => d.ipAddress))
            .ForMember(s => s.logUsername, o => o.MapFrom(d => d.logUsername))
            ;
        ;
    }
}
