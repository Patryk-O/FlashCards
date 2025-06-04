using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain.Entities;

namespace FlashCards.Application.UseCases.Cards.GetCardById;

public record GetCardByIdQuery(Guid CardId) : IQuery<Card>;