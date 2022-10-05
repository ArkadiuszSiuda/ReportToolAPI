using ReportToolAPI.Dtos;

namespace ReportToolAPI.Interfaces;

public interface IBaseRepository<T, TDto> where T : class
{
    Task<List<TDto>> Get();

    Task<TDto> Get(Guid id);

    Task<TDto> Create(TDto tDto);

    Task<TDto> Update(TDto tDto, Guid id);

    Task Delete(Guid id);
}