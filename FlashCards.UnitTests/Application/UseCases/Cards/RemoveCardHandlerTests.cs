using FlashCards.Application.Common;
using FlashCards.Application.UseCases.Cards.CreateCard;
using FlashCards.Application.UseCases.Cards.RemoveCard;
using FlashCards.Application.UseCases.Cards.UpdateCard;
using FlashCards.Domain.Entities;
using FlashCards.UnitTests.TestUtilities;
using JetBrains.Annotations;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Cards;

[TestSubject(typeof(RemoveCardHandler))]
public class RemoveCardHandlerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public RemoveCardHandlerTests(TestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetMocks();
    }
    [Fact]
    public async Task HandlerAsync_Valid_ShouldRemoveCard()
    {
        //Arrange
        var card = _fixture.TestCards.FirstOrDefault();

        var handler = new RemoveCardHandler(_fixture.DeckRepository.Object, _fixture.CardRepository.Object);
        var command = new RemoveCardCommand(card.Id);
        //Act
        var result = await handler.Handle(command);
        
        //Assert
        _fixture.CardRepository.Verify(r => r.RemoveCardAsync(card), Times.Once);

    }
    [Fact]
    public async Task HandleAsync_DeckNotFound_ShouldThrow()
    {
        //Arrange


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
    [Fact]
    public async Task HandleAsync_CardNotFound_ShouldThrow()
    {
        //Arrange

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
