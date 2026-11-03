using System;

namespace Booking.API.Middleware
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiKeyAuthorizationAttribute : Attribute
    {
    }
}
