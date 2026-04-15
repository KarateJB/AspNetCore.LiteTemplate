using domain.Entities;
using MediatR;

namespace application.Features.Members.Queries;

public record FindMemberQuery(Guid Id) : IRequest<Member?>;
