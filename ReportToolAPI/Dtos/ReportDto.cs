using ReportToolAPI.Entities;

namespace ReportToolAPI.Dtos;

public class ReportDto
{
    public Guid Id { get; set; }
    public string Comment { get; set; }

    public string ToReproduce { get; set; }

    public int Reproducibility { get; set; }

    public Guid CodeId { get; set; }

    public Guid ProductId { get; set; }
}