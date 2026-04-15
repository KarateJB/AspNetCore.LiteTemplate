namespace domain.ValueObjects;

public record MemberId(Guid Value)
{
    public static MemberId New() => new(Guid.NewGuid());

    public static MemberId From(Guid value) => value == Guid.Empty
        ? throw new ArgumentException("Member ID cannot be empty", nameof(value))
        : new(value);
}
