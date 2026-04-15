using application.Features.Members.Commands;
using application.Interfaces;
using MediatR;

namespace application.Features.Members.Handlers;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IMemberService _memberService;

    public DeleteMemberCommandHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        return _memberService.DeleteAsync(request.Id, cancellationToken);
    }
}
