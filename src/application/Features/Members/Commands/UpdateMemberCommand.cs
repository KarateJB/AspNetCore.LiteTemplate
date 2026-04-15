using domain.Entities;
using MediatR;

namespace application.Features.Members.Commands;

public record UpdateMemberCommand(Member Member) : IRequest<bool>;
