using FlashCards.Application.UseCases.Cards.CreateCard;
using FlashCards.Application.UseCases.Cards.GetAllCards;
using FlashCards.Domain.Entities;
using FlashCards.UnitTests.TestUtilities;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Cards;

public class GetAllCardsHandlerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public GetAllCardsHandlerTests(TestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetMocks();
    }

    [Fact]
    public async Task GetAllCardsHandler_ReturnsAllCards_FromOneDeck()
    {
        // Arrange
        var deck = Deck.CreateNewDeck("Simple Deck for mock");
        var cards = new List<Card>
        {
            Card.CreateNewCard(deck.Id, "Q1", "A1"),
            Card.CreateNewCard(deck.Id, "Q2", "A2"),
            Card.CreateNewCard(deck.Id, "Q3", "A3"),
            Card.CreateNewCard(deck.Id, "Q4", "A4"),
            Card.CreateNewCard(deck.Id, "Q5", "A5")
        };

        _fixture.CardRepository
            .Setup(r => r.GetAllCards())
            .ReturnsAsync(cards);

        var handler = new GetAllCardsHandler(_fixture.CardRepository.Object);

        // Act
        var result = await handler.Handle(new GetAllCardsQuery());

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(5, result.Value.Count());
    }
}