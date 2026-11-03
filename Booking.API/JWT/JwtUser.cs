using Booking.Application;
using System.Collections.Generic;

namespace Booking.API.JWT
{
    public class JwtUser : IApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public IEnumerable<string> AllowedUseCases { get; set; } = new List<string>();
    }
}
