using FlashCards.Domain;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;
using FlashCards.Domain.ValueObject;

namespace FlashCards.Application.UseCases.Decks.CreateDeck;

public class CreateDeckHandler : ICreateDeckHandler
{
    private readonly IDeckRepository _repository;
    public CreateDeckHandler(IDeckRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> HandleAsync(CreateDeckCommand command)
    {
        var deck = Deck.CreateNewDeck(command.Title);
        await _repository.AddDeckAsync(deck);
        return deck.Id;
    }
}