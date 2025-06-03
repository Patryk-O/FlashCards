using FlashCards.Domain.Entities;

namespace FlashCards.Domain.Repositories;

public interface ICardRepository
{
    Task AddCardAsync(Card? card);
    Task<Card?> GetCard(Guid id);
    Task<IEnumerable<Card>> GetAllCards();
    Task RemoveCard(Card card);
    Task UpdateCard(Card card);
}