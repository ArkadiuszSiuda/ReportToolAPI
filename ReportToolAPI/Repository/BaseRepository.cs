using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class BaseRepository<T, TDto> : IBaseRepository<T, TDto> where T : BaseEntity, new() where TDto : BaseDto
{
    private readonly ReportContext _context;
    private readonly IMapper _mapper;
    private DbSet<T> table;

    public BaseRepository(ReportContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        table = _context.Set<T>();
    }

    public async Task<TDto> Create(TDto entityDto)
    {
        var dbEntity = new T();

        if (entityDto == null)
            throw new NullEntityException();

        var entity = await table.FirstOrDefaultAsync(r => r.Id == entityDto.Id);

        if (entity != null)
            throw new EntityExistException();

        _mapper.Map(entityDto, dbEntity);
        await table.AddAsync(dbEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<TDto>(dbEntity);
    }

    public async Task Delete(Guid id)
    {
        var entity = await table.FirstOrDefaultAsync(r => r.Id == id);
        if (entity != null)
        {
            table.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<List<TDto>> Get()
    {
        return _mapper.Map<List<TDto>>(await table.ToListAsync());
    }

    public async Task<TDto> Get(Guid id)
    {
        return _mapper.Map<TDto>(await table.FirstOrDefaultAsync(r => r.Id == id));
    }

    public async Task<TDto> Update(TDto entityDto, Guid id)
    {
        if (entityDto == null || id == Guid.Empty)
            throw new NullEntityException();

        entityDto.Id = id;

        var entity = await table.FirstOrDefaultAsync(r => r.Id == id);

        if (entity == null)
            throw new NullEntityException();

        _mapper.Map(entityDto, entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<TDto>(entity);
    }
}