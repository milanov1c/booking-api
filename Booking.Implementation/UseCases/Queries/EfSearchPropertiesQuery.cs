using Booking.Application;
using Booking.Application.DTO;
using Booking.Application.Extensions;
using Booking.Application.Queries;
using Booking.DataAccess;
using Booking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Booking.Implementation.UseCases.Queries;

public class EfSearchPropertiesQuery : EfUseCase, ISearchPropertiesQuery
{
    public EfSearchPropertiesQuery(BookingDbContext context) : base(context)
    {
    }

    public PagedResponse<PropertyListItemDTO> Execute(PropertySearchDTO param)
    {
        IQueryable<Property> query = ctx.Properties;

        if (!string.IsNullOrWhiteSpace(param.Name))
        {
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{param.Name}%"));
        }

        if (!string.IsNullOrWhiteSpace(param.City))
        {
            query = query.Where(p => EF.Functions.ILike(p.Destination.City, $"%{param.City}%"));
        }

        if (!string.IsNullOrWhiteSpace(param.Country))
        {
            query = query.Where(p => EF.Functions.ILike(p.Destination.Country.Name, $"%{param.Country}%"));
        }

        if (param.MinCapacity.HasValue)
        {
            query = query.Where(p => p.PropertyRooms.Any(r => r.Capacity >= param.MinCapacity.Value));
        }

        if (param.MinPrice.HasValue || param.MaxPrice.HasValue)
        {
            query = query.Where(p => p.PropertyRooms.Any(r => r.PropertyRoomPrices.Any(pr =>
                (!param.MinPrice.HasValue || pr.Price >= param.MinPrice.Value) &&
                (!param.MaxPrice.HasValue || pr.Price <= param.MaxPrice.Value))));
        }

        var projected = query
            .OrderBy(p => p.Name)
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
                Amenities = p.AmenityProperties.Select(x=>x.Amenity.Name).ToList()

            });

        return projected.ToPagedResponse(param.Page, param.PageSize);
    }

    public string Name => "Search Properties";
    public string Id => "ef-search-properties";
}
