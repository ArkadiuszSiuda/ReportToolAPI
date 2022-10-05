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
    public CodesRepository(ReportContext context, IMapper mapper) : base(context, mapper)

    {
    }
}