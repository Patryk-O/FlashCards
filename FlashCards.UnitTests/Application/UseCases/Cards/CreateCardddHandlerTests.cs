using FlashCards.Application.UseCases.Cards.CreateCard;
using FlashCards.Domain.Entities;
using FlashCards.UnitTests.TestUtilities;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Cards;

public class CreateCardHandlerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public CreateCardHandlerTests(TestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetMocks();
    }
    [Fact]
    public async Task HandlerAsync_Valid_ShouldCreateCard()
    {
        //Arrange
        var deck = Deck.CreateNewDeck("Simple Deck for mock");
        _fixture.DeckRepository.Setup(r => r.GetAsync(deck.Id)).ReturnsAsync(deck);
        

        var handler = new CreateCardToDeckHandler(_fixture.DeckRepository.Object, _fixture.CardRepository.Object);
        var command = new CreateCardToDeckCommand(deck.Id, "Q", "A");
        //Act
        var result = await handler.HandleCommandAsync(command);
        //Assert
        Assert.NotEqual(Guid.Empty, result);
    }
    [Fact]
    public async Task HandleAsync_DeckNotFound_ShouldThrow()
    {
        _fixture.DeckRepository.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Deck?)null);
        var handler = new CreateCardToDeckHandler(_fixture.DeckRepository.Object, _fixture.CardRepository.Object);

        var command = new CreateCardToDeckCommand(Guid.NewGuid(), "Q", "A");

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleCommandAsync(command));
        Assert.Equal("Deck not found", ex.Message);
    }
}