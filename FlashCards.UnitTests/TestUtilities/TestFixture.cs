using FlashCards.Domain.Repositories;
using Moq;

namespace FlashCards.UnitTests.TestUtilities;

public class TestFixture
{
    public Mock<ICardRepository> CardRepository { get; }
    public Mock<IDeckRepository> DeckRepository { get; }

    public TestFixture()
    {
        CardRepository = new Mock<ICardRepository>();
        DeckRepository = new Mock<IDeckRepository>();
    }

    public void ResetMocks()
    {
        CardRepository.Reset();
        DeckRepository.Reset();
    }
}