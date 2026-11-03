namespace Booking.Application;

public interface IUploadedFile
{
    public string FileName { get; }
    public string ContentType { get; }
    public long Length { get; }
    public Stream OpenReadStream();
}
