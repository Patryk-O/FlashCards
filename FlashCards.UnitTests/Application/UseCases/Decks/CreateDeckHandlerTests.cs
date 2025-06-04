using FlashCards.Application.UseCases.Decks.CreateDeck;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;
using FlashCards.UnitTests.TestUtilities;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Decks;

public class CreateDeckHandlerTests : IClassFixture<TestFixture>
{
    [Fact]
    public async Task CreateDeckHandler_ShouldCreateDeck()
    {
        //Arrange

        var repositoryMock = new Mock<IDeckRepository>();
        repositoryMock.Setup(r => r.AddDeckAsync(It.IsAny<Deck>())).Returns(Task.CompletedTask);
        
        var handle = new CreateDeckHandler(repositoryMock.Object);
        var command = new CreateDeckCommand("Simple Deck");
        
        // Act
        var result = await handle.Handle(command);
        
        //Assert
        Assert.NotEqual(Guid.Empty, result.Value);
        repositoryMock.Verify(r => r.AddDeckAsync(It.Is<Deck>(d => d.Title != null && d.Title == "Simple Deck")), Times.Once);
    }
    [Fact]
    public async Task Handle_ValidTitle_ReturnsSuccessResult()
    {
        var repository = new Mock<IDeckRepository>();
        var handler = new CreateDeckHandler(repository.Object);
        var command = new CreateDeckCommand("Valid Title");

        var result = await handler.Handle(command);

        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public async Task Handle_InvalidTitle_ReturnsFailureResult()
    {
        var repository = new Mock<IDeckRepository>();
        var handler = new CreateDeckHandler(repository.Object);
        var command = new CreateDeckCommand(""); // invalid

        var result = await handler.Handle(command);

        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.StartsWith("Title is empty", result.Error!.message);
    }

}