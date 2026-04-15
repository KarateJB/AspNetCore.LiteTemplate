using domain.Entities;

namespace application.Interfaces;

public interface IMemberRepository
{
    Task<Guid> CreateAsync(Member member, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Member member, CancellationToken cancellationToken = default);

    Task<Member?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
