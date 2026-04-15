using domain.Models;

namespace application.Interfaces;

public interface IMemberService
{
    Task<Guid> CreateAsync(MemberDataModel member, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(MemberDataModel member, CancellationToken cancellationToken = default);

    Task<MemberDataModel?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
