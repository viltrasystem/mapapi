﻿using static System.Net.WebRequestMethods;

namespace ViltrapportenApi.Modal
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string  Issuer  { get; set; }
        public string Audience { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }
}
