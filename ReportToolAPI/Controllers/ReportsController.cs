using CsvHelper;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportToolAPI.Dtos;
using ReportToolAPI.Interfaces;
using System.Data;

namespace ReportToolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportsRepository _reportsRepository;

    public ReportsController(IReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReportDto>>> Get()
    {
        return Ok(await _reportsRepository.Get());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReportDto>> Get(Guid id)
    {
        return await _reportsRepository.Get(id) is ReportDto r ? Ok(r) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ReportDto>> Create([FromBody] ReportDto reportDto)
    {
        var report = await _reportsRepository.Create(reportDto);

        if (report == null)
            return BadRequest();

        return Ok(report);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReportDto>> Update([FromRoute] Guid id, [FromBody] ReportDto reportDto)
    {
        var report = await _reportsRepository.Update(reportDto, id);

        if (report == null)
            return BadRequest();

        return Ok(report);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _reportsRepository.Delete(id);
        return NoContent();
    }

    [HttpGet("Export")]
    public async Task<ActionResult> ExportReport()
    {
        using var memoryStream = new MemoryStream();
        var reportCsv = await _reportsRepository.Get();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(reportCsv);
        csvWriter.Flush();
        var memStr = memoryStream.ToArray();
        return File(memStr, "text/csv", "Reports.csv");
    }
}