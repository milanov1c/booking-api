using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Queries;
using Booking.DataAccess;

namespace Booking.Implementation.UseCases.Queries;

public class EfGetUserProfileQuery : EfUseCase, IGetUserProfileQuery
{
    private readonly IApplicationUser _user;

    public EfGetUserProfileQuery(BookingDbContext context, IApplicationUser user) : base(context)
    {
        _user = user;
    }

    public UserProfileDTO Execute(GetUserProfileDTO param)
    {
        var baseInfo = ctx.Users
            .Where(u => u.Id == _user.Id)
            .Select(u => new { u.Id, u.FirstName, u.LastName, u.Email })
            .FirstOrDefault();

        if (baseInfo == null)
        {
            return null;
        }

        var profile = new UserProfileDTO
        {
            Id = baseInfo.Id,
            FirstName = baseInfo.FirstName,
            LastName = baseInfo.LastName,
            Email = baseInfo.Email,
            Role = _user.Role,
        };

        if (string.Equals(_user.Role, "Tenant", StringComparison.OrdinalIgnoreCase))
        {
            profile.Reservations = ctx.Reservations
                .Where(r => r.UserId == _user.Id)
                .OrderByDescending(r => r.StartDate)
                .Select(r => new ReservationDTO
                {
                    Id = r.Id,
                    PropertyId = r.PropertyRoom.PropertyId,
                    PropertyName = r.PropertyRoom.Property.Name,
                    PropertyRoomId = r.PropertyRoomId,
                    PropertyRoomName = r.PropertyRoom.Name,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalPrice = r.TotalPrice,
                    Status = r.Status.ToString(),
                })
                .ToList();

            profile.Reviews = ctx.Reviews
                .Where(r => r.UserId == _user.Id)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    UserFullName = baseInfo.FirstName + " " + baseInfo.LastName,
                    PropertyId = r.PropertyId,
                    PropertyName = r.Property.Name,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                })
                .ToList();
        }
        else if (string.Equals(_user.Role, "Landlord", StringComparison.OrdinalIgnoreCase)
                 || string.Equals(_user.Role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            // Admins can manage properties too, but only ones they personally
            // own - same scope as a Landlord, not every property on the platform.
            profile.OwnedProperties = ctx.Properties
                .Where(p => p.OwnerId == _user.Id)
                .Select(p => new PropertyListItemDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Address = p.Address,
                    PropertyType = p.PropertyType.Name,
                    City = p.Destination.City,
                    Country = p.Destination.Country.Name,
                    ImagePath = p.Attachments.Select(a => a.Path).FirstOrDefault(),
                    PropertyRoomsNumber = p.PropertyRooms.Count,
                    LowestPrice = p.PropertyRooms.SelectMany(x => x.PropertyRoomPrices).Select(pr => (decimal?)pr.Price).Min() ?? 0,
                    HighestPrice = p.PropertyRooms.SelectMany(x => x.PropertyRoomPrices).Select(pr => (decimal?)pr.Price).Max() ?? 0,
                    LowestCapacity = p.PropertyRooms.Select(x => (int?)x.Capacity).Min() ?? 0,
                    HighestCapacity = p.PropertyRooms.Select(x => (int?)x.Capacity).Max() ?? 0,
                    Rating = p.Reviews.Any() ? (decimal?)p.Reviews.Average(x => x.Rating) : null,
                })
                .ToList();
        }

        return profile;
    }

    public string Name => "Get User Profile";
    public string Id => "ef-get-user-profile";
}
