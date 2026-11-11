using MediatR;

namespace OnlineMuhasebeServer.Application.Messaging
{
	public interface ICommand<out TReponse>:IRequest<TReponse>
	{

	}
}
