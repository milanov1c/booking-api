namespace Booking.Application.DTO;

public class UserListItemDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    public bool IsActive { get; set; }
}
