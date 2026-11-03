namespace Booking.Application.DTO;

public class PropertyReviewsSearchDTO
{
    public int PropertyId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
