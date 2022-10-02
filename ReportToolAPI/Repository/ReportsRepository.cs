using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class ReportsRepository : IReportsRepository
{
    private readonly ReportContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;

    public ReportsRepository(ReportContext context, IMapper mapper, ICurrentUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<List<ReportDto>> Get()
    {
        return _mapper.Map<List<ReportDto>>(await _context.Reports.Where(y => y.OwnerId == Guid.Parse(_userService.UserId!)).ToListAsync());
    }

    public async Task<ReportDto> Get(Guid id)
    {
        return _mapper.Map<ReportDto>(
            await _context.Reports.FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == Guid.Parse(_userService.UserId!)));
    }

    public async Task<ReportDto> Create(ReportDto reportDto)
    {
        var dbReport = new Report();

        if (reportDto == null)
            throw new NullEntityException();

        var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == reportDto.Id);

        if (report != null)
            throw new EntityExistException();

        _mapper.Map(reportDto, dbReport);
        await _context.Reports.AddAsync(dbReport);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReportDto>(dbReport);
    }

    public async Task Delete(Guid id)
    {
        var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == Guid.Parse(_userService.UserId!));
        if (report != null)
        {
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ReportDto> Update(ReportDto reportDto, Guid id)
    {
        if (reportDto == null || id == Guid.Empty)
            throw new NullEntityException();

        reportDto.Id = id;

        var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == Guid.Parse(_userService.UserId!));

        if (report == null)
            throw new NullEntityException();

        _mapper.Map(reportDto, report);
        _context.Reports.Update(report);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReportDto>(report);
    }
}