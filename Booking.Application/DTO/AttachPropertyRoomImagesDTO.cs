using Booking.Application;

namespace Booking.Application.DTO;

public class AttachPropertyRoomImagesDTO
{
    public int PropertyRoomId { get; set; }
    public List<IUploadedFile> Images { get; set; } = new();
}
