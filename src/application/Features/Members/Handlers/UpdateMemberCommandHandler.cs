using application.Features.Members.Commands;
using application.Interfaces;
using MediatR;

namespace application.Features.Members.Handlers;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IMemberService _memberService;

    public UpdateMemberCommandHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        return _memberService.UpdateAsync(request.Member, cancellationToken);
    }
}
