using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class ReportsRepository : BaseRepository<Report, ReportDto>, IReportsRepository
{
    public ReportsRepository(ReportContext context, IMapper mapper) : base(context, mapper)
    {
    }
}