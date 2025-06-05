using FlashCards.Application.UseCases.Cards.UpdateCard;
using FlashCards.Application.UseCases.Decks.CreateDeck;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;
using FlashCards.UnitTests.TestUtilities;
using JetBrains.Annotations;
using Moq;

namespace FlashCards.UnitTests.Application.UseCases.Decks;
[TestSubject(typeof(CreateDeckHandlerTests))]
public class CreateDeckHandlerTests : IClassFixture<TestFixture>
{
    private readonly FlashCards.UnitTests.TestUtilities.TestFixture _testFixture;

    public CreateDeckHandlerTests(FlashCards.UnitTests.TestUtilities.TestFixture testFixture)
    {
        _testFixture = testFixture;
        _testFixture.ResetMocks();
    }

    [Fact]
    public async Task CreateDeckHandler_ShouldCreateDeck()
    {
        //Arrange
        var handle = new CreateDeckHandler(_testFixture.DeckRepository.Object);
        var command = new CreateDeckCommand("Simple Deck");
        
        // Act
        var result = await handle.Handle(command);
        
        //Assert
        _testFixture.DeckRepository.Verify(r => 
            r.AddDeckAsync(It.Is<Deck>(d => d.Title != null && d.Title == "Simple Deck")), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ValidTitle_ReturnsSuccessResult()
    {
        //Arrange
        var handler = new CreateDeckHandler(_testFixture.DeckRepository.Object);
        var command = new CreateDeckCommand("Valid Title");
        //Act
        var result = await handler.Handle(command);
        //Assert
        _testFixture.DeckRepository.Verify(r => r.AddDeckAsync(It.Is<Deck>(d => d.Title != null && d.Title == "Valid Title")), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidTitle_ReturnsFailureResult()
    {
        //Arrange

        var handler = new CreateDeckHandler(_testFixture.DeckRepository.Object);
        var command = new CreateDeckCommand(""); // invalid
        //Act
        var result = await handler.Handle(command);
        //Assert
        _testFixture.DeckRepository.Verify(r => r.AddDeckAsync(It.IsAny<Deck>()), Times.Never);
        Assert.False(result.Success);
        Assert.Equal("Title is empty", result.Error.message);
    }

}