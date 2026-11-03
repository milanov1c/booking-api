namespace Booking.Application.DTO;

public class LandlordStatsDTO
{
    public int TotalProperties { get; set; }
    public int TotalPropertyRooms { get; set; }
    public int TotalReservations { get; set; }
    public int ConfirmedReservations { get; set; }
    public int PendingReservations { get; set; }
    public int CancelledReservations { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal? AverageRating { get; set; }
}
