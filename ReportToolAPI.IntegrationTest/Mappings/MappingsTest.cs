using AutoMapper;
using FluentAssertions;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Mapper;
using System.Runtime.Serialization;

namespace ReportToolAPI.IntegrationTest.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<CodeProfile>();
            config.AddProfile<ReportProfile>();
            config.AddProfile<ProductProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }

    [Theory]
    [InlineData(typeof(Code), typeof(CodeDto))]
    [InlineData(typeof(Report), typeof(ReportDto))]
    [InlineData(typeof(Product), typeof(ProductDto))]
    public void Mapper_CorrectMappings_ShoudlSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        var mappedObject = _mapper.Map(instance, source, destination);

        mappedObject.GetType().Should().Be(destination);
    }

    private static object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        return FormatterServices.GetUninitializedObject(type);
    }
}