using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class ReportsRepository : IReportsRepository
{
    private readonly ReportContext _context;
    private readonly IMapper _mapper;

    public ReportsRepository(ReportContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReportDto>> Get()
    {
        return _mapper.Map<List<ReportDto>>(await _context.Reports.ToListAsync());
    }

    public async Task<ReportDto> Get(Guid id)
    {
        return _mapper.Map<ReportDto>(await _context.Reports.FirstOrDefaultAsync(r => r.Id == id));
    }

    public async Task<ReportDto> Create(ReportDto reportDto)
    {
        var dbReport = new Report();

        if (reportDto == null)
            return null;

        var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == reportDto.Id);

        if (report != null)
            return null;

        _mapper.Map(reportDto, dbReport);
        await _context.Reports.AddAsync(dbReport);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReportDto>(dbReport);
    }

    public async Task Delete(Guid id)
    {
        _context.Reports.Remove(await _context.Reports.FirstOrDefaultAsync(r => r.Id == id));
        await _context.SaveChangesAsync();
    }

    public async Task<ReportDto> Update(ReportDto reportDto, Guid id)
    {
        if (reportDto == null || id == Guid.Empty)
            return null;

        reportDto.Id = id;

        var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);

        if (report == null)
            return null;

        _mapper.Map(reportDto, report);
        _context.Reports.Update(report);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReportDto>(report);
    }
}