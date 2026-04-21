using Mediator;

namespace application.Features.Members.Commands;

public record DeleteMemberCommand(Guid Id) : IRequest<bool>;
