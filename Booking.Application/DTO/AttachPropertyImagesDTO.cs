using Booking.Application;

namespace Booking.Application.DTO;

public class AttachPropertyImagesDTO
{
    public int PropertyId { get; set; }
    public List<IUploadedFile> Images { get; set; } = new();
}
