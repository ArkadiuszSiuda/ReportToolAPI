using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportToolAPI.Dtos;
using ReportToolAPI.Interfaces;
using ReportToolAPI.Repository;

namespace ReportToolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductsRepository _productsRepository;

    public ProductsController(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> Get()
    {
        return Ok(await _productsRepository.Get());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get(Guid id)
    {
        return await _productsRepository.Get(id) is ProductDto r ? Ok(r) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto productDto)
    {
        var product = await _productsRepository.Create(productDto);

        if (product == null)
            return BadRequest();

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Update([FromRoute] Guid id, [FromBody] ProductDto productDto)
    {
        var product = await _productsRepository.Update(productDto, id);

        if (product == null)
            return BadRequest();

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _productsRepository.Delete(id);
        return NoContent();
    }
}