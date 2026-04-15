using application.Features.Members.Queries;
using application.Interfaces;
using domain.Entities;
using MediatR;

namespace application.Features.Members.Handlers;

public class FindMemberQueryHandler : IRequestHandler<FindMemberQuery, Member?>
{
    private readonly IMemberService _memberService;

    public FindMemberQueryHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public Task<Member?> Handle(FindMemberQuery request, CancellationToken cancellationToken)
    {
        return _memberService.FindAsync(request.Id, cancellationToken);
    }
}
