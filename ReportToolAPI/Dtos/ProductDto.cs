namespace ReportToolAPI.Dtos;

public class ProductDto : BaseDto
{
    public string Company { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}