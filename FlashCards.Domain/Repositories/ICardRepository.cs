using FlashCards.Domain.Entities;

namespace FlashCards.Domain.Repositories;

public interface ICardRepository
{
    Task AddCardAsync(Card? card);
    Task<Card?> GetCardByIdAsync(Guid id);
    Task<IReadOnlyCollection<Card>> GetAllCardsAsync();
    Task RemoveCardAsync(Card card);
    Task UpdateCardAsync(Guid cardId,Card card);
}