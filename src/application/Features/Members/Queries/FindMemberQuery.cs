using domain.Models;
using MediatR;

namespace application.Features.Members.Queries;

public record FindMemberQuery(Guid Id) : IRequest<MemberDataModel?>;
