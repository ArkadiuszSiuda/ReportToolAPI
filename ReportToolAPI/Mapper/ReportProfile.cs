using AutoMapper;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;

namespace ReportToolAPI.Mapper;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<ReportDto, Report>().ReverseMap();
    }
}