namespace FlashCards.Domain.ValueObject;

public abstract class RequiredText
{
    public string Value { get; }

    protected RequiredText(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or empty.", nameof(value));
        if (value.Length > maxLength)
            throw new ArgumentException($"Value cannot be longer than {maxLength} characters.", nameof(value));
        Value = value.Trim();
    }
    public override string ToString() => Value;
    
    public override bool Equals(object? obj) =>
        obj is RequiredText other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
    public static implicit operator string(RequiredText title) => title.Value;
}