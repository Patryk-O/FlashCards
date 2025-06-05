using FlashCards.Application.Abstractions;
using FlashCards.Domain.Entities;

namespace FlashCards.Application.UseCases.Decks.GetDeckByID;

public record GetDeckByIdQuery(Guid deckId) : IQuery<Deck>

{
    
}