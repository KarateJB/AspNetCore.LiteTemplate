using domain.ValueObjects;

namespace domain.Entities;

public class Member
{
    public MemberId? Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateOnly Birthday { get; set; }

    public Address? Address { get; set; }

    public Phone? Phone { get; set; }

    public DateTimeOffset RegisterOn { get; set; }

    public bool IsEnabled { get; set; } = true;
}
