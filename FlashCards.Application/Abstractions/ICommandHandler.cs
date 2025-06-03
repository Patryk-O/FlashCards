namespace FlashCards.Application.Abstractions;

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> HandleCommandAsync(TCommand command);
}