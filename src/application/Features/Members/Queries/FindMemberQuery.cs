using domain.Entities;
using Mediator;

namespace application.Features.Members.Queries;

public record FindMemberQuery(Guid Id) : IRequest<Member?>;
