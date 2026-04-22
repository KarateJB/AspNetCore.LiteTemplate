namespace domain.ValueObjects;

public record MemberId(Guid Value)
{
    public static MemberId New() => new(Guid.NewGuid());

    public static MemberId From(Guid? guid) => guid is null || guid == Guid.Empty
        ? throw new ArgumentException("Member ID cannot be null or empty", nameof(guid))
        : new(guid!.Value);
}
