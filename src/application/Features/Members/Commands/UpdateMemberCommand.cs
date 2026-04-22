using domain.Entities;
using Mediator;

namespace application.Features.Members.Commands;

public record UpdateMemberCommand(Member Member) : IRequest<bool>;
