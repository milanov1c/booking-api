namespace Booking.Application;

public interface IQuery<TParam, TResponse> : IUseCase
    where TResponse : class
{
    TResponse Execute(TParam param);
}