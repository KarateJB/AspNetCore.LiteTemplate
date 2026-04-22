using application.Features.Members.Commands;
using application.Interfaces;
using Mediator;

namespace application.Features.Members.Handlers;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberService _memberService;

    public CreateMemberCommandHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public async ValueTask<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        return await _memberService.CreateAsync(request.Member, cancellationToken);
    }
}
