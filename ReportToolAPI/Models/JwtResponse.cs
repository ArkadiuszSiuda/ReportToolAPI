﻿namespace ReportToolAPI.Models;

public class JwtResponse
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }
}