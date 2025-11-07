namespace PD421_Dashboard_WEB_API.BLL.Settings
{
    public class JwtSettings
    {
        // Відправник токену
        public string Issuer { get; set; } = string.Empty;
        // Отримувач токену
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get;set; } = string.Empty;
        public int ExpiresInHours { get; set; }
    }
}
