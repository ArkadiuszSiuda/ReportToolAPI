using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportToolAPI.Db;
using ReportToolAPI.Dtos;
using ReportToolAPI.Entities;
using ReportToolAPI.Exceptions;
using ReportToolAPI.Interfaces;

namespace ReportToolAPI.Repository;

public class ProductsRepository : IProductsRepository
{
    private readonly ReportContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;

    public ProductsRepository(ReportContext context, IMapper mapper, ICurrentUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<List<ProductDto>> Get()
    {
        return _mapper.Map<List<ProductDto>>(await _context.Products.ToListAsync());
    }

    public async Task<ProductDto> Get(Guid id)
    {
        return _mapper.Map<ProductDto>(
            await _context.Products.FirstOrDefaultAsync(r => r.Id == id));
    }

    public async Task<ProductDto> Create(ProductDto productDto)
    {
        var dbProdcut = new Product();

        if (productDto == null)
            throw new NullEntityException();

        var product = await _context.Products.FirstOrDefaultAsync(r => r.Id == productDto.Id);

        if (product != null)
            throw new EntityExistException();

        _mapper.Map(productDto, dbProdcut);
        await _context.Products.AddAsync(dbProdcut);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDto>(dbProdcut);
    }

    public async Task Delete(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(r => r.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ProductDto> Update(ProductDto dbProdcut, Guid id)
    {
        if (dbProdcut == null || id == Guid.Empty)
            throw new NullEntityException();

        dbProdcut.Id = id;

        var product = await _context.Products.FirstOrDefaultAsync(r => r.Id == id);

        if (product == null)
            throw new NullEntityException();

        _mapper.Map(dbProdcut, product);
        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }
}