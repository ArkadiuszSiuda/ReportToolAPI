namespace ReportToolAPI.Entities;

public class Report : OwnedEntity
{
    public string Comment { get; set; }

    public string ToReproduce { get; set; }

    public int Reproducibility { get; set; }

    public Guid CodeId { get; set; }

    public Code? Code { get; set; }

    public Guid ProductId { get; set; }

    public Product? Product { get; set; }
}