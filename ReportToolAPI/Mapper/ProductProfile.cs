using AutoMapper;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;

namespace ReportToolAPI.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
    }
}