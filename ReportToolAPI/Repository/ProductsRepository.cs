using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class ProductsRepository : BaseRepository<Product, ProductDto>, IProductsRepository
{
    private readonly ReportContext _context;

    public ProductsRepository(ReportContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<int> Affects(Guid id)
    {
        return await _context.Reports.CountAsync(r => r.ProductId == id);
    }
}