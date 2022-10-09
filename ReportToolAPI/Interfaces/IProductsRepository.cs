using ReportToolAPI.Dtos;

namespace ReportToolAPI.Interfaces;

public interface IProductsRepository
{
    Task<List<ProductDto>> Get();

    Task<ProductDto> Get(Guid id);

    Task<ProductDto> Create(ProductDto prodcutDto);

    Task<ProductDto> Update(ProductDto productDto, Guid id);

    Task Delete(Guid id);

    Task<int> Affects(Guid id);
}