using FlashCards.Application.Abstractions;
using FlashCards.Domain;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Cards.CreateCard;

public class CreateCardToDeckHandler : ICommandHandler<CreateCardToDeckCommand, Guid>
{
    public readonly IDeckRepository _deckRepository;
    public readonly ICardRepository _cardRepository;

    public CreateCardToDeckHandler(IDeckRepository deckRepository, ICardRepository cardRepository)
    {
        _deckRepository = deckRepository;
        _cardRepository = cardRepository;
    }

    public async Task<Guid> HandleCommandAsync(CreateCardToDeckCommand command)
    {
        var deck = await _deckRepository.GetAsync(command.DeckId) ?? throw new ArgumentException("Deck not found");
        
        var card = Card.CreateNewCard(deck.Id, command.Question, command.Answer);
        await _cardRepository.AddCardAsync(card);
        
        return card.Id;
    }
}