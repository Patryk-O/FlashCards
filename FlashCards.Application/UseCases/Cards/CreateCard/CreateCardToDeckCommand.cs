using FlashCards.Application.Abstractions;

namespace FlashCards.Application.UseCases.Cards.CreateCard;

public record CreateCardToDeckCommand(Guid DeckId, string Question,string Answer) : ICommand<Guid>;