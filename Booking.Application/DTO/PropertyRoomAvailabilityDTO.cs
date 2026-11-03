namespace Booking.Application.DTO;

public class PropertyRoomAvailabilityDTO
{
    public int PropertyRoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    /// <summary>
    /// True if no non-cancelled reservation overlaps this date range.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// True if a price has been set for every night in the requested range.
    /// A room can be "available" (not booked) but still not bookable if a
    /// price is missing for one or more of the nights.
    /// </summary>
    public bool PriceIsSet { get; set; }
}
