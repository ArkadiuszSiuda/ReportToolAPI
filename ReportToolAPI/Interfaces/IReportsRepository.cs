using ReportToolAPI.Dtos;

namespace ReportToolAPI.Interfaces;

public interface IReportsRepository
{
    Task<List<ReportDto>> Get();

    Task<ReportDto> Get(Guid id);

    Task<ReportDto> Create(ReportDto reportDto);

    Task<ReportDto> Update(ReportDto reportDto, Guid id);

    Task Delete(Guid id);
}