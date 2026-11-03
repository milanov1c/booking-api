namespace Booking.Application.DTO;

public class UserProfileDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    // Populated only when Role == "Tenant"
    public List<ReservationDTO> Reservations { get; set; }
    public List<ReviewDTO> Reviews { get; set; }

    // Populated only when Role == "Landlord"
    public List<PropertyListItemDTO> OwnedProperties { get; set; }
}
