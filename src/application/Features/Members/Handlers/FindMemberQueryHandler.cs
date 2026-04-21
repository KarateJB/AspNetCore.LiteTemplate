using application.Features.Members.Queries;
using application.Interfaces;
using domain.Entities;
using Mediator;

namespace application.Features.Members.Handlers;

public class FindMemberQueryHandler : IRequestHandler<FindMemberQuery, Member?>
{
    private readonly IMemberService _memberService;

    public FindMemberQueryHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public async ValueTask<Member?> Handle(FindMemberQuery request, CancellationToken cancellationToken)
    {
        return await _memberService.FindAsync(request.Id, cancellationToken);
    }
}
