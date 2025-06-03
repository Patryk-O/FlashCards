namespace FlashCards.Domain.ValueObject;

public class DeckTitle : RequiredText
{
    DeckTitle(string title) : base(title, 50){}
    
    public static implicit operator string(DeckTitle title) => title.Value;
    public static implicit operator DeckTitle(string title) => new DeckTitle(title);
}