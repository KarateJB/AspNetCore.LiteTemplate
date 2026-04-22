using domain.Entities;
using Mediator;

namespace application.Features.Members.Commands;

public record CreateMemberCommand(Member Member) : IRequest<Guid>;
