using System.Runtime.InteropServices.JavaScript;
using FlashCards.Application.Common;
using FlashCards.Application.UseCases.Cards.CreateCard;
using FlashCards.Domain.Entities;
using FlashCards.UnitTests.TestUtilities;
using JetBrains.Annotations;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Cards;
[TestSubject(typeof(CreateCardHandlerTests))]
public class CreateCardHandlerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public CreateCardHandlerTests(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public async Task HandlerAsync_Valid_ShouldCreateCard()
    {
        //Arrange
        var deck = _fixture.TestDecks.First();
        _fixture.CardRepository.Setup(r => r.AddCardAsync(It.IsAny<Card>()))
            .Callback<Card>(card => _fixture.TestCards.Add(card));
        _fixture.DeckRepository.Setup(r => r.GetAsync(It.IsAny<Guid>())).Returns(() => Task.FromResult(deck));

        var handler = new CreateCardToDeckHandler(_fixture.DeckRepository.Object, _fixture.CardRepository.Object);
        var command = new CreateCardToDeckCommand(deck.Id, "Q", "A");
        //Act
        await handler.Handle(command);

        //Assert
        _fixture.CardRepository.Verify(r => r.AddCardAsync(It.IsAny<Card>()), Times.Once);
        Assert.Equal(7 , _fixture.TestCards.Count);
    }

    [Fact]
    public async Task HandleAsync_DeckNotFound_ShouldThrow()
    {
        //Arrange
        var handler = new CreateCardToDeckHandler(_fixture.DeckRepository.Object, _fixture.CardRepository.Object);
        var command = new CreateCardToDeckCommand(Guid.NewGuid(), "Q", "A");
        //Act
        var result = await handler.Handle(command);
        //Assert
        _fixture.CardRepository.Verify(r => r.AddCardAsync(It.IsAny<Card>()), Times.Never);
        Assert.False(result.Success);
        Assert.Equal(ErrorCode.Validation, result.Error?.code);
        Assert.Equal("Deck not found", result.Error?.message);
    }
}