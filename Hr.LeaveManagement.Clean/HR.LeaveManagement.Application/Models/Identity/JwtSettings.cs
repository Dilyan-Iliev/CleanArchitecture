﻿namespace HR.LeaveManagement.Application.Models.Identity
{
    public class JwtSettings
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public double DurationMinutes { get; set; }
    }
}
