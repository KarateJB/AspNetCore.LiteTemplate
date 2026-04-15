using domain.Models;
using MediatR;

namespace application.Features.Members.Commands;

public record CreateMemberCommand(MemberDataModel Member) : IRequest<Guid>;
