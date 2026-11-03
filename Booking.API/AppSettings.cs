using System.Collections.Generic;
using Booking.DataAccess.Seeding;
namespace Booking.API
{
    public class AppSettings
    {
        public string FromEmail { get; set; }
        public string ConnString { get; set; }
        public IEnumerable<string> ApiKeys { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public SeedSettings Seed { get; set; } = new();
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int DurationSeconds { get; set; }
        public int RefreshTokenHours { get; set; }
    }
}
