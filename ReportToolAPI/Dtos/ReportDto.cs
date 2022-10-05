using ReportToolAPI.Entities;

namespace ReportToolAPI.Dtos;

public class ReportDto : BaseDto
{
    public string Comment { get; set; } = string.Empty;

    public string ToReproduce { get; set; } = string.Empty;

    public int Reproducibility { get; set; }

    public Guid CodeId { get; set; }

    public Guid ProductId { get; set; }
}