using FlashCards.Domain.ValueObject;

namespace FlashCards.Domain.Entities;

public class Card
{
    public Guid Id { get; private set; }
    public CardQuestion? Question { get; private set; }
    public CardAnswer? Answer { get; private set; }
    public Guid DeckId { get; private set; }

    private Card(Guid deckId,CardQuestion question, CardAnswer answer)
    {
        if (deckId.Equals(Guid.Empty))
            throw new ArgumentException("DeckId cannot be empty.");
        
        Id = Guid.NewGuid();
        DeckId = deckId;
        Answer = answer;
        Question = question;
    }
    
    public static Card? CreateNewCard(Guid deckId,string question, string answer)
    {
        return new Card(deckId, question, answer);
    }

    public void AddToDeck(Deck? deck)
    {
        ArgumentNullException.ThrowIfNull(deck, nameof(deck));
        DeckId = deck.Id;
    }

    public Guid GetDeckId()
    {
        return DeckId;
    }
    
    public void SetQuestion(CardQuestion newQuestion)
    {
        if(string.IsNullOrWhiteSpace(newQuestion))
            throw new ArgumentException("Question cannot be null or empty.");
        this.Question = newQuestion;
    }
    
    public void SetAnswer(CardAnswer newAnswer)
    {
        this.Answer = newAnswer;
    }


}