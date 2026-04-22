using System.Diagnostics.CodeAnalysis;

namespace domain.ValueObjects;

public record Phone
{
    public required string Value { get; set; }

    [SetsRequiredMembers]
    public Phone(string value)
    {
        this.Value = ValidateAndNormalize(value);
    }

    private static string ValidateAndNormalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone cannot be empty", nameof(value));
        if (value.Length > 20)
            throw new ArgumentException("Phone cannot exceed 20 characters", nameof(value));

        return value.Trim();
    }
}
