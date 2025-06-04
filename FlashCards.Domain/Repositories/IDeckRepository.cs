using FlashCards.Domain.Entities;

namespace FlashCards.Domain.Repositories;

public interface IDeckRepository
{
    Task AddDeckAsync(Deck deck);
    Task<Deck?> GetAsync(Guid id);
    Task<IReadOnlyCollection<Deck>> GetAllAsync();
    Task RemoveDeckAsync(Deck deck);
    Task RemoveCardFromDeckAsync(Deck deck, Card card);
    Task UpdateAsync(Deck deck);
}