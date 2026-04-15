using domain.Models;
using MediatR;

namespace application.Features.Members.Commands;

public record UpdateMemberCommand(MemberDataModel Member) : IRequest<bool>;
