namespace FlashCards.Domain.ValueObject;

public class CardAnswer : RequiredText
{
    CardAnswer(string answer) : base(answer, 100){}
    
    public static implicit operator string(CardAnswer answer) => answer.Value;
    public static implicit operator CardAnswer(string answer) => new CardAnswer(answer);
}