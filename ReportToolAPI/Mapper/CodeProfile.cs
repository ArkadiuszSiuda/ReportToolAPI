using AutoMapper;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;

namespace ReportToolAPI.Mapper;

public class CodeProfile : Profile
{
    public CodeProfile()
    {
        CreateMap<CodeDto, Code>().ReverseMap();
    }
}