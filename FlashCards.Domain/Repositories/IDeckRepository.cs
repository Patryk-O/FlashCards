using FlashCards.Domain.Entities;

namespace FlashCards.Domain.Repositories;

public interface IDeckRepository
{
    Task AddDeckAsync(Deck deck);
    Task<Deck?> GetAsync(Guid id);
    Task<IEnumerable<Deck>> GetAllAsync();
    Task RemoveAsync(Deck deck);
    Task UpdateAsync(Deck deck);
}