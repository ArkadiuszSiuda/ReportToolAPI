namespace ReportToolAPI.Entities;

public class Product : BaseEntity
{
    public string Company { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}