namespace Booking.Application;

public interface ICommand<TRequest> : IUseCase

{
    public void Execute(TRequest request);
}