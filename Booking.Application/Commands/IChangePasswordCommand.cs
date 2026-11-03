using Booking.Application.DTO;

namespace Booking.Application.Commands;

public interface IChangePasswordCommand : ICommand<ChangePasswordDTO>
{
}
