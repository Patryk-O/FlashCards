using FlashCards.Domain.Entities;

namespace FlashCards.UnitTests.Domain.Entities;

public class CardTests
{

    [Fact]
    public void Constructor_ShouldGenerateIdAndSetTitle()
    {
        var card = Card.CreateNewCard(Guid.NewGuid(), question: " Test Question ",answer: " Test Answer ");
        
        Assert.NotEqual(Guid.Empty, card.Id);
        Assert.Equal("Test Question", card.Question);
        Assert.Equal("Test Answer", card.Answer);
        Assert.NotEqual(Guid.Empty, card.DeckId);

    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void SetQuestion_Invalid_ShouldThrow(string invalidQuestion)
    {
        var ex = Assert.Throws<ArgumentException>(() => Card.CreateNewCard(Guid.NewGuid(), question: invalidQuestion, answer: " Test Answer "));
        Assert.StartsWith("Value cannot be null or empty.", ex.Message);
    }

    [Fact]
    public void SetQuestion_Valid_ShouldUpdateQuestion()
    {
        var card = Card.CreateNewCard(Guid.NewGuid(), question: " Test Question ", answer: " Test Answer ");
        card.SetQuestion("Updated Question");

        Assert.Equal("Updated Question", card.Question);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void SetAnswer_Invalid_ShouldThrow(string invalidAnswer)
    {
        var ex = Assert.Throws<ArgumentException>(() => Card.CreateNewCard(Guid.NewGuid(), question: " Test Question ", answer: invalidAnswer));
        Assert.StartsWith("Value cannot be null or empty.", ex.Message);
    }
    
    [Fact]
    public void SetAnswer_Valid_ShouldUpdateAnswer()
    {
        var card = Card.CreateNewCard(Guid.NewGuid(), question: " Test Question ", answer: " Test Answer ");
        card.SetQuestion("Updated Answer");

        Assert.Equal("Updated Answer", card.Question);
    }

    [Fact]
    public void SetDeck_Valid_ShouldUpdateDeck()
    {
        var card = Card.CreateNewCard(Guid.NewGuid(), question: " Test Question ", answer: " Test Answer ");
        var deck = Deck.CreateNewDeck("Initial");
        card.AddToDeck(deck);
        Assert.Equal(deck.Id, card.GetDeckId());
    }
    [Fact]
    public void SetDeck_InValid_ShouldUpdateDeck()
    {
        var ex = Assert.Throws<ArgumentException>( () => Card.CreateNewCard(Guid.Empty, question: " Test Question ", answer: " Test Answer "));
        Assert.Equal("DeckId cannot be empty.", ex.Message);
    }
}