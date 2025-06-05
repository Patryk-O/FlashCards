using FlashCards.Application.Abstractions;
using FlashCards.Domain.Entities;

namespace FlashCards.Application.UseCases.Decks.GetAllDecks;

public record GetAllDecksQuery : IQuery<IReadOnlyCollection<Deck>>
{
    
}