using FlashCards.Application.UseCases.Cards.GetCardById;
using FlashCards.UnitTests.TestUtilities;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Decks;

public class GetDeckHandlerTests: IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public GetDeckHandlerTests(TestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResetMocks();
    }

    [Fact]
    public async Task GetCardByIdHandler_ReturnOneSpecificCard_FromOneDeck()
    {
        // Arrange
 
        var card = _fixture.TestCards.First();
        var handler = new GetCardByIdHandler(_fixture.CardRepository.Object);
        var command = new GetCardByIdQuery(card.Id); 
        // Act
        var result = await handler.Handle(command);

        // Assert
        _fixture.CardRepository.Verify(r => r.GetCardByIdAsync(It.IsAny<Guid>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
    }
    [Fact]
    public async Task GetCardByIdHandler_CardDoesNotExist_ReturnsNull()
    {

        // Arrange
        var card = _fixture.TestCards.First();
        var handler = new GetCardByIdHandler(_fixture.CardRepository.Object);

        // Act
        var result = await handler.Handle(new GetCardByIdQuery(Guid.Empty));
        _fixture.CardRepository.Verify(r => r.GetCardByIdAsync(card.Id), Times.Never);
        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
    }
}