using FlashCards.Domain.ValueObject;

namespace FlashCards.Domain.Entities;

public class Deck
{
    public Guid Id { get; private set; }
    public DeckTitle?  Title { get; private set; }
    private readonly List<Card?> _cards =  new List<Card?>();
    public IReadOnlyCollection<Card?> Cards => _cards;

    private Deck(DeckTitle title)
    {
        ArgumentNullException.ThrowIfNull(title, nameof(title));
        Id = Guid.NewGuid();
        Title = title;
    }

    public static Deck CreateNewDeck(DeckTitle title)
    {
        return new Deck(title);
    }

    public void SetTitle(DeckTitle title)
    {
        ArgumentNullException.ThrowIfNull(title, nameof(title));
        Title = title;
    }

    public void AddCard(Card? card)
    {
        ArgumentNullException.ThrowIfNull(card, nameof(card));
        _cards.Add(card);
    }

    public void RemoveCard(Card? card)
    {
        ArgumentNullException.ThrowIfNull(card, nameof(card));
        _cards.Remove(card);
    }
}
