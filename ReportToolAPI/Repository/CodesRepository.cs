using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class CodesRepository : BaseRepository<Code, CodeDto>, ICodesRepository
{
    private readonly ReportContext _context;

    public CodesRepository(ReportContext context, IMapper mapper) : base(context, mapper)

    {
        _context = context;
    }

    public async Task<int> Affects(Guid id)
    {
        return await _context.Reports.CountAsync(r => r.CodeId == id);
    }
}