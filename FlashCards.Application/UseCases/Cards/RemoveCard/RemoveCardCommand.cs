using FlashCards.Application.Abstractions;

namespace FlashCards.Application.UseCases.Cards.RemoveCard;

public record RemoveCardCommand(Guid CardId) : ICommand<Guid>;