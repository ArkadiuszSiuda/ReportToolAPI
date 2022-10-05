using ReportToolAPI.Dtos;

namespace ReportToolAPI.Interfaces;

public interface ICodesRepository
{
    Task<List<CodeDto>> Get();

    Task<CodeDto> Get(Guid id);

    Task<CodeDto> Create(CodeDto codeDto);

    Task<CodeDto> Update(CodeDto codeDto, Guid id);

    Task Delete(Guid id);
}