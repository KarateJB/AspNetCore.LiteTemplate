using application.Features.Members.Commands;
using application.Interfaces;
using Mediator;

namespace application.Features.Members.Handlers;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IMemberService _memberService;

    public DeleteMemberCommandHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public async ValueTask<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        return await _memberService.DeleteAsync(request.Id, cancellationToken);
    }
}
