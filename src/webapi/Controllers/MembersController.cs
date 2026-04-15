using application.Features.Members.Commands;
using application.Features.Members.Queries;
using AutoMapper;
using domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MembersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberRequest request)
    {
        var member = _mapper.Map<MemberDataModel>(request);
        var id = await _mediator.Send(new CreateMemberCommand(member));
        return CreatedAtAction(nameof(Find), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateMemberRequest request)
    {
        var member = _mapper.Map<MemberDataModel>(request);
        member.Id = id;

        var updated = await _mediator.Send(new UpdateMemberCommand(member));
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MemberResponse>> Find(Guid id)
    {
        var member = await _mediator.Send(new FindMemberQuery(id));
        if (member is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<MemberResponse>(member);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteMemberCommand(id));
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
