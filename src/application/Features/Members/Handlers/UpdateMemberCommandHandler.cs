using application.Features.Members.Commands;
using application.Interfaces;
using Mediator;

namespace application.Features.Members.Handlers;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IMemberService _memberService;

    public UpdateMemberCommandHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public async ValueTask<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        return await _memberService.UpdateAsync(request.Member, cancellationToken);
    }
}
