using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class ReportsRepository : BaseRepository<Report, ReportDto>, IReportsRepository
{
    private readonly ReportContext _context;
    private readonly IMapper _mapper;

    public ReportsRepository(ReportContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public override async Task<List<ReportDto>> Get()
    {
        return _mapper.Map<List<ReportDto>>(await _context.Reports
            .Include(r => r.Code)
            .Include(r => r.Product)
            .ToListAsync());
    }
}