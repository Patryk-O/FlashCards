namespace FlashCards.Application.UseCases.Decks.CreateDeck;

public interface ICreateDeckHandler
{
    Task<Guid> HandleAsync(CreateDeckCommand command);
}