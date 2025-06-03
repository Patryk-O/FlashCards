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
        var result = await handle.HandleAsync(command);
        
        //Assert
        Assert.NotEqual(Guid.Empty, result);
        repositoryMock.Verify(r => r.AddDeckAsync(It.Is<Deck>(d => d.Title != null && d.Title == "Simple Deck")), Times.Once);
    }
}