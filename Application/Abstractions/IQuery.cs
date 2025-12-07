using MediatR;

namespace Application.Abstractions
{
    internal interface IQuery { }
    internal interface IQuery<out TResponse> : IRequest<TResponse> { }
}