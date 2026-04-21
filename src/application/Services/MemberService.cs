using application.Interfaces;
using domain.Entities;

namespace application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Guid> CreateAsync(Member member, CancellationToken cancellationToken = default)
    {
        member.RegisterOn = member.RegisterOn == default ? DateTimeOffset.UtcNow : member.RegisterOn;
        return await _memberRepository.CreateAsync(member, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Member member, CancellationToken cancellationToken = default)
    {
        var existingMember = await _memberRepository.FindAsync(member.Id!.Value, cancellationToken);
        if (existingMember is null)
        {
            return false;
        }

        member.RegisterOn = existingMember.RegisterOn;
        return await _memberRepository.UpdateAsync(member, cancellationToken);
    }

    public Task<Member?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _memberRepository.FindAsync(id, cancellationToken);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _memberRepository.DeleteAsync(id, cancellationToken);
    }
}
