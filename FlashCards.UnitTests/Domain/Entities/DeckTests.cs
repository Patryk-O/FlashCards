using FlashCards.Domain.Entities;

namespace FlashCards.UnitTests.Domain.Entities;

public class DeckTests 
{
    [Fact]
    public void Constructor_ShouldGenerateIdAndSetTitle()
    {
        var deck =  Deck.CreateNewDeck(" Test Deck ");
        
        Assert.NotEqual(Guid.Empty, deck.Id);
        Assert.Equal("Test Deck", deck.Title!);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void SetTitle_Invalid_ShouldThrow(string invalidTitle)
    {
        var ex = Assert.Throws<ArgumentException>(() => Deck.CreateNewDeck(invalidTitle));
        Assert.StartsWith("Value cannot be null or empty.", ex.Message);
    }

    [Fact]
    public void SetTitle_Valid_ShouldUpdateTitle()
    {
        var deck = Deck.CreateNewDeck("Initial");
        deck.SetTitle("Updated Title");

        Assert.Equal("Updated Title", deck.Title?.ToString());
    }

    [Fact]
    public void AddCard_ShouldAddCard()
    {
        var deck = Deck.CreateNewDeck("Initial");
        var card = Card.CreateNewCard(Guid.NewGuid(), question: " Test Question ",answer: " Test Answer ");
        deck.AddCard(card);
        Assert.Single(deck.Cards);
    }
    
    [Fact]
    public void AddCard_ShouldNotAddCard()
    {
        var deck = Deck.CreateNewDeck("Initial");
        Card? card = null;
        var ex = Assert.Throws<ArgumentException>(() => deck.AddCard(card));
        Assert.Equal("Card cannot be null.", ex.Message);
    }
    
}