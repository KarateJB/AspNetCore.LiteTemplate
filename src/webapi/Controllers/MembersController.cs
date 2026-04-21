using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using application.Mapping;
using application.Features.Members.Commands;
using application.Features.Members.Contracts;
using application.Features.Members.Queries;
using domain.ValueObjects;
using shared.Configurations;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
[FeatureGate(FeatureFlags.Member)]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly MemberMapper _mapper;

    public MembersController(IMediator mediator, MemberMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberRequest request, CancellationToken cancellationToken)
    {
        var member = _mapper.MapForCreate(request);
        var id = await _mediator.Send(new CreateMemberCommand(member), cancellationToken);
        return CreatedAtAction(nameof(Find), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        var member = _mapper.MapForUpdate(request);
        member.Id = MemberId.From(id);

        var updated = await _mediator.Send(new UpdateMemberCommand(member), cancellationToken);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MemberResponse>> Find(Guid id, CancellationToken cancellationToken)
    {
        var member = await _mediator.Send(new FindMemberQuery(id), cancellationToken);
        if (member is null)
        {
            return NotFound();
        }

        var response = _mapper.MapToResponse(member);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteMemberCommand(id), cancellationToken);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
