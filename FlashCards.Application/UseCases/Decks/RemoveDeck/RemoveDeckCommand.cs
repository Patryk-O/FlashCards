using System.Windows.Input;
using FlashCards.Application.Abstractions;
using FlashCards.Domain.Entities;

namespace FlashCards.Application.UseCases.Decks.RemoveDeck;

public record RemoveDeckCommand(Guid DeckId) : ICommand<Guid>
{
    
}