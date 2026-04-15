using domain.Entities;
using MediatR;

namespace application.Features.Members.Commands;

public record CreateMemberCommand(Member Member) : IRequest<Guid>;
