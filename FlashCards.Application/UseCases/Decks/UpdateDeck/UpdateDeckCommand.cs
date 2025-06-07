using FlashCards.Application.Abstractions;
using FlashCards.Domain.ValueObject;

namespace FlashCards.Application.UseCases.Decks.UpdateDeck;

public record UpdateDeckCommand(Guid DeckId, DeckTitle title) : ICommand<Guid>;