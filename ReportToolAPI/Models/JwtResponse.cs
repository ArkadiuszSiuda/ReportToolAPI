namespace ReportToolAPI.Models;

public class JwtResponse
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }

    public List<string> Roles { get; set; } = new List<string>();
}