using FlashCards.Application.Abstractions;

namespace FlashCards.Application.UseCases.Decks.CreateDeck;

public record CreateDeckCommand(string Title) : ICommand<Guid>;
