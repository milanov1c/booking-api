namespace Booking.Application.DTO;

public class GetUserReservationsDTO
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
