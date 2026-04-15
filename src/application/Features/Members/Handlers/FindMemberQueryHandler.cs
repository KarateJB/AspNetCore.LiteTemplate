using application.Features.Members.Queries;
using application.Interfaces;
using domain.Models;
using MediatR;

namespace application.Features.Members.Handlers;

public class FindMemberQueryHandler : IRequestHandler<FindMemberQuery, MemberDataModel?>
{
    private readonly IMemberService _memberService;

    public FindMemberQueryHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public Task<MemberDataModel?> Handle(FindMemberQuery request, CancellationToken cancellationToken)
    {
        return _memberService.FindAsync(request.Id, cancellationToken);
    }
}
