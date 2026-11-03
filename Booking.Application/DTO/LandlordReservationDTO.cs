namespace Booking.Application.DTO;

public class LandlordReservationDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tenant { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PropertyRoomName { get; set; }
    public string Status {get;set;}
    
}