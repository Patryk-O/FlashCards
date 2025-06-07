using FlashCards.Application.Abstractions;
using FlashCards.Domain.Entities;
using FlashCards.Domain.ValueObject;

namespace FlashCards.Application.UseCases.Cards.UpdateCard;

public record UpdateCardCommand(Guid CardId, Guid DeckId, CardQuestion Question,CardAnswer Answer) : ICommand<Guid>;