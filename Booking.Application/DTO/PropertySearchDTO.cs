namespace Booking.Application.DTO;

public class PropertySearchDTO
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int? MinCapacity { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
