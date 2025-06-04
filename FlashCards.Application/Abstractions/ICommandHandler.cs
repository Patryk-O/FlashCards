using FlashCards.Application.Common;

namespace FlashCards.Application.Abstractions;

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<UseCaseResult<TResult>> Handle(TCommand command);
}

public interface ICommandHandler<TCommand> : ICommandHandler<TCommand, Unit> where TCommand : ICommand<Unit>
{
}