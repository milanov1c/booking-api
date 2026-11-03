using System;

namespace Booking.API.ExceptionLogging
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex);
    }
}
