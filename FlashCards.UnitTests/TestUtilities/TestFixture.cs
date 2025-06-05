using FlashCards.Application.Common;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;
using Moq;

namespace FlashCards.UnitTests.TestUtilities;

public class TestFixture
{
    public Mock<ICardRepository> CardRepository { get; }
    public Mock<IDeckRepository> DeckRepository { get; }
    public List<Deck> TestDecks { get; private set; } = new();
    public List<Card> TestCards { get; private set; } = new();

    public TestFixture()
    {
        SetupTestData();
        CardRepository = new Mock<ICardRepository>();
        DeckRepository = new Mock<IDeckRepository>();
        SetupMocks();

    }
    private void SetupTestData()
    {
        var deck = Deck.CreateNewDeck("Test Deck");
        
        TestDecks = new List<Deck>
        {
            Deck.CreateNewDeck("Test Deck 1"),
            Deck.CreateNewDeck("Test Deck 2"),
            Deck.CreateNewDeck("Test Deck 3"),
            Deck.CreateNewDeck("Test Deck 4")
        };
        var cards = new List<Card>
        {
            Card.CreateNewCard(TestDecks[0].Id, "Q1", "A1"),
            Card.CreateNewCard(TestDecks[0].Id, "Q2", "A2"),
            Card.CreateNewCard(TestDecks[0].Id, "Q3", "A3"),
            Card.CreateNewCard(TestDecks[1].Id, "Q4", "A4"),
            Card.CreateNewCard(TestDecks[1].Id, "Q5", "A5"),
            Card.CreateNewCard(TestDecks[2].Id, "Q6", "A6"),
        };

        TestDecks[0].AddCard(cards[0]);
        TestDecks[0].AddCard(cards[1]);
        TestDecks[0].AddCard(cards[2]);
        
        TestDecks[1].AddCard(cards[3]);
        TestDecks[1].AddCard(cards[4]);
        
        TestDecks[2].AddCard(cards[5]);
        
        TestCards = cards;
    }

    private void SetupMocks()
    {
        CardRepository.Setup(r => r.AddCardAsync(It.IsAny<Card>()))
            .Callback<Card>(card => TestCards.Add(card));

        CardRepository.Setup(r => r.RemoveCardAsync(It.IsAny<Card>()))
            .Callback<Card>(card =>
            {
                TestCards.Remove(card);
            })
            .Returns(Task.FromResult(TestCards.FirstOrDefault()));
        
        CardRepository.Setup(r => r.GetCardByIdAsync(It.IsAny<Guid>()))
            .Returns<Guid>((cardId) => Task.FromResult(TestCards.Find(x => x.Id == cardId)));
        CardRepository.Setup(r => r.GetAllCardsAsync())
            .Returns(() => Task.FromResult((IReadOnlyCollection<Card>)TestCards.AsReadOnly()));
        
        CardRepository.Setup(r => r.UpdateCardAsync(It.IsAny<Guid>(),It.IsAny<Card>()))
            .Callback<Guid, Card>((cardId, Card) =>
            {
                TestCards.Remove(TestCards.First(x => x.Id == cardId));
                TestCards.Add(Card);
            })
            .Returns(Task.CompletedTask);
        

        DeckRepository.Setup(r => r.GetAsync(It.IsAny<Guid>()))
            .Returns<Guid>((deckId) => Task.FromResult(TestDecks.Find(x => x.Id == deckId)));
        DeckRepository.Setup(r => r.GetAllAsync())
            .Returns(() => Task.FromResult((IReadOnlyCollection<Card>)TestDecks.AsReadOnly()));
        DeckRepository.Setup(r => r.AddDeckAsync(It.IsAny<Deck>()))
            .Callback<Deck>(deck => TestDecks.Add(deck))
            .Returns(Task.CompletedTask);
        DeckRepository.Setup(r => r.RemoveDeckAsync(It.IsAny<Deck>()))
            .Callback<Deck>(deck =>
            {
                TestDecks.Remove(deck);
            })
            .Returns(Task.FromResult(TestCards.FirstOrDefault()));
        DeckRepository.Setup(r => r.UpdateAsync(It.IsAny<Deck>()))
            .Callback<Deck>(deck =>
            {
                TestDecks.Remove(deck);
            })
            .Returns(Task.FromResult(TestCards.FirstOrDefault()));


    }

    public void ResetMocks()
    {
        SetupTestData();
        SetupMocks();
    }
}