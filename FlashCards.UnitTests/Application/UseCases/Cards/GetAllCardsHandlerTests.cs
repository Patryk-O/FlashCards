using FlashCards.Application.UseCases.Cards.CreateCard;
using FlashCards.Application.UseCases.Cards.GetAllCards;
using FlashCards.Domain.Entities;
using FlashCards.UnitTests.TestUtilities;
using JetBrains.Annotations;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Cards;
[TestSubject(typeof(GetAllCardsHandlerTests))]
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

        var handler = new GetAllCardsHandler(_fixture.CardRepository.Object);
        var command = new GetAllCardsQuery();
        // Act
        var result = await handler.Handle(command);

        // Assert
        _fixture.CardRepository.Verify(r => r.GetAllCardsAsync(), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(6, result.Value.Count);
    }
}