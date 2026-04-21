namespace infrastructure.Repositories.Models;

internal class MemberDataModel
{
    public Guid? Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // Use DateTime instead of DateOnly for compatibility with Dapper and PostgreSQL date type
    public DateTime Birthday { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateTimeOffset RegisterOn { get; set; }

    public bool IsEnabled { get; set; }
}
