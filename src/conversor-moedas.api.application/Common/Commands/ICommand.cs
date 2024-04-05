using conversor_moedas.domain.Shared;
using MediatR;

namespace conversor_moedas.api.application.Common.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}