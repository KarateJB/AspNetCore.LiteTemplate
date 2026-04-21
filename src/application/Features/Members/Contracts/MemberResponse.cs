namespace application.Features.Members.Contracts;

public class MemberResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Birthday { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string RegisterOn { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }
}
