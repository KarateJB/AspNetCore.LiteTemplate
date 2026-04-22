namespace application.Features.Members.Contracts;

public class CreateMemberRequest
{
    public string Name { get; set; } = string.Empty;

    public DateOnly Birthday { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;
}
