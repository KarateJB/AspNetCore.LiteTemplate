using System.Diagnostics.CodeAnalysis;

namespace domain.ValueObjects;

public record Address
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public Address(string value)
    {
        this.Value = ValidateAndNormalize(value);
    }

    private static string ValidateAndNormalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Address cannot be empty", nameof(value));
        if (value.Length > 100)
            throw new ArgumentException("Address cannot exceed 100 characters", nameof(value));

        return value.Trim();
    }
}
