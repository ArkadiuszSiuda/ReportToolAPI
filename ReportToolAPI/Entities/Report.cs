namespace ReportToolAPI.Entities;

public class Report : OwnedEntity
{
    public string Comment { get; set; } = string.Empty;

    public string ToReproduce { get; set; } = string.Empty;

    public int Reproducibility { get; set; }

    public Guid CodeId { get; set; }

    public Code? Code { get; set; }

    public Guid ProductId { get; set; }

    public Product? Product { get; set; }
}