namespace ReportToolAPI.IntegrationTest.Configuration;

public static class ApiRoutes
{
    public static string Base { get; set; } = "/api";

    public static string Codes { get; set; } = Base + "/codes";
    public static string Code { get; set; } = Base + "/codes/{id}";

    public static string Products { get; set; } = Base + "/products";
    public static string Product { get; set; } = Base + "/products/{id}";

    public static string Reports { get; set; } = Base + "/reports";
    public static string Report { get; set; } = Base + "/reports/{id}";

    public static string Login { get; set; } = Base + "/login";
    public static string Register { get; set; } = Base + "/register";
}