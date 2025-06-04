using FlashCards.Application.Abstractions;
using FlashCards.Domain.Entities;

namespace FlashCards.Application.UseCases.Cards.UpdateCard;

public record UpdateCardCommand(Guid CardId, Guid DeckId, string Question,string Answer) : ICommand<Guid>;