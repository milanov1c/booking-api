namespace Booking.Application.DTO;

public class PropertyListItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PropertyType { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ImagePath { get; set; }
    public int PropertyRoomsNumber { get; set; }
    public decimal LowestPrice { get; set; }
    public decimal HighestPrice { get; set; }
    public int LowestCapacity { get; set; }
    public int HighestCapacity { get; set; }
    public decimal? Rating { get; set; }
    public IEnumerable<string> Amenities { get; set; }
    
}
