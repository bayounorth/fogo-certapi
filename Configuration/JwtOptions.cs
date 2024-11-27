namespace FogoCertApi.Configuration
{
    public class JwtOptions
    {
        public const string ConfigKey = "Jwt";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }
}
