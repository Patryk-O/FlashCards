using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain.Entities;

namespace FlashCards.Application.UseCases.Cards.GetAllCards;

public record GetAllCardsQuery() : IQuery<IReadOnlyCollection<Card>>;