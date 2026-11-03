namespace Booking.Application.DTO;

public class GetUsersSearchDTO
{
    public string Email { get; set; }
    public string RoleName { get; set; }
    public bool? IsActive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
