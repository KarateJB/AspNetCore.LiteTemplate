namespace domain.Models;

public class MemberDataModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTimeOffset Birthday { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateTimeOffset RegisterOn { get; set; }

    public bool IsEnabled { get; set; } = true;
}
