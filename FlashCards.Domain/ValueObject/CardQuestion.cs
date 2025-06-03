namespace FlashCards.Domain.ValueObject;

public class CardQuestion : RequiredText
{
    CardQuestion(string Question) : base(Question, 100){}
    
    public static implicit operator CardQuestion(string Question) => new CardQuestion(Question);
    public static implicit operator string(CardQuestion Question) =>  Question.Value;
}