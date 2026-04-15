using application.Features.Members.Commands;
using application.Interfaces;
using MediatR;

namespace application.Features.Members.Handlers;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberService _memberService;

    public CreateMemberCommandHandler(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        return _memberService.CreateAsync(request.Member, cancellationToken);
    }
}
