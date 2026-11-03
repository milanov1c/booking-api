using Booking.DataAccess;

namespace Booking.Implementation.UseCases;

public abstract class EfUseCase
{
    protected readonly BookingDbContext ctx;

    protected EfUseCase(BookingDbContext context)
    {
        ctx = context;
    }
}