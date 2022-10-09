using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportToolAPI.Dtos;
using ReportToolAPI.Interfaces;
using ReportToolAPI.Repository;

namespace ReportToolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CodesController : ControllerBase
{
    private readonly ICodesRepository _codesRepository;

    public CodesController(ICodesRepository codesRepository)
    {
        _codesRepository = codesRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<CodeDto>>> Get()
    {
        return Ok(await _codesRepository.Get());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CodeDto>> Get(Guid id)
    {
        return await _codesRepository.Get(id) is CodeDto r ? Ok(r) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<CodeDto>> Create([FromBody] CodeDto codeDto)
    {
        var code = await _codesRepository.Create(codeDto);

        if (code == null)
            return BadRequest();

        return Ok(code);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CodeDto>> Update([FromRoute] Guid id, [FromBody] CodeDto codeDto)
    {
        var code = await _codesRepository.Update(codeDto, id);

        if (code == null)
            return BadRequest();

        return Ok(code);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _codesRepository.Delete(id);
        return NoContent();
    }

    [HttpGet("affects/{id}")]
    public async Task<ActionResult<int>> Affects([FromRoute] Guid id)
    {
        return Ok(await _codesRepository.Affects(id));
    }
}