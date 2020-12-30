using System;


namespace eShopWithReact.Services.UserAuthentication.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public TimeSpan TokenTTL { get; set; }
        // refresh token time to live, inactive tokens are automatically deleted from the database after this time
        public int RefreshTokenTTL { get; set; }
    }
}
