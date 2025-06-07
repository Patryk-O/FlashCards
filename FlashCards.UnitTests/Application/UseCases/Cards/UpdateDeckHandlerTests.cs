using FlashCards.Application.Common;
using FlashCards.Application.UseCases.Cards.CreateCard;
using FlashCards.Application.UseCases.Cards.UpdateCard;
using FlashCards.Domain.Entities;
using FlashCards.UnitTests.TestUtilities;
using JetBrains.Annotations;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Cards;

[TestSubject(typeof(UpdateCardHandler))]
public class UpdateDeckHandlerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public UpdateDeckHandlerTests(TestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetMocks();
    }
    [Fact]
    public async Task HandlerAsync_Valid_ShouldUpdateCard()
    {
        //Arrange
        var card = _fixture.TestCards.First();

        var handler = new UpdateCardHandler(_fixture.DeckRepository.Object, _fixture.CardRepository.Object);
        var command = new UpdateCardCommand(card.Id, _fixture.TestDecks[2].Id,"Updated Q", "Updated A");
        //Act
        var result = await handler.Handle(command);
        
        //Assert
        _fixture.CardRepository.Verify(r => r.UpdateCardAsync(card.Id,It.IsAny<Card>()), Times.Once);
        Assert.True(result.Success);
        Assert.Equal(card.Id, result.Value);
        Assert.Equal("Updated Q", _fixture.TestCards.Last().Question);
        Assert.Equal("Updated A", _fixture.TestCards.Last().Answer);
        Assert.Equal(_fixture.TestDecks[2].Id, _fixture.TestCards.Last().DeckId);
        Assert.NotEqual(card.DeckId, _fixture.TestCards.Last().DeckId);
        Assert.NotEqual(card.Answer, _fixture.TestCards.Last().Answer);
        Assert.NotEqual(card.Question, _fixture.TestCards.Last().Question);
    }
    [Fact]
    public async Task HandleAsync_DeckNotFound_ShouldThrow()
    {
        //Arrange
        _fixture.DeckRepository
            .Setup(r => r.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Deck?)null);

        var handler = new CreateCardToDeckHandler(_fixture.DeckRepository.Object, _fixture.CardRepository.Object);
        var command = new CreateCardToDeckCommand(Guid.NewGuid(), "Q", "A");
        //Act
        var result = await handler.Handle(command);
        _fixture.CardRepository.Verify(r => r.AddCardAsync(It.IsAny<Card>()), Times.Never);
        //Assert
        Assert.False(result.Success);
        Assert.Equal(ErrorCode.Validation, result.Error?.code);
        Assert.Equal("Deck not found", result.Error?.message);
    }
}