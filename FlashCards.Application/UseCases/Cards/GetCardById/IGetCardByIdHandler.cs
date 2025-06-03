using FlashCards.Domain;
using FlashCards.Domain.Entities;

namespace FlashCards.Application.UseCases.Cards.GetCardById;

public interface IGetCardByIdHandler
{
    Task<Card> GetCardById(Guid cardId);
}