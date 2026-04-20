using System.Data;
using application.Interfaces;
using Dapper;
using domain.Entities;
using domain.ValueObjects;
using Microsoft.Extensions.Options;
using Npgsql;
using shared.Configurations;

namespace infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly string _connectionString;

    public MemberRepository(IOptions<AppSettings> appSettingsOptions)
    {
        AppSettings appSettings = appSettingsOptions.Value;
        _connectionString = appSettings.ConnectionStrings.PgConnection
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }
    }

    public async Task<Guid> CreateAsync(Member member, CancellationToken cancellationToken = default)
    {
        var memberData = MapMemberToData(member);

        const string sql = @"
    INSERT INTO ""Members"" (""Id"", ""Name"", ""Birthday"", ""Address"", ""Phone"", ""RegisterOn"", ""IsEnabled"")
    VALUES (@Id, @Name, @Birthday, @Address, @Phone, @RegisterOn, @IsEnabled)
    RETURNING ""Id"";";

        using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<Guid>(new CommandDefinition(sql, memberData, cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateAsync(Member member, CancellationToken cancellationToken = default)
    {
        var memberData = MapMemberToData(member);

        const string sql = @"
UPDATE ""Members""
SET ""Name"" = @Name,
    ""Birthday"" = @Birthday,
    ""Address"" = @Address,
    ""Phone"" = @Phone,
    ""RegisterOn"" = @RegisterOn,
    ""IsEnabled"" = @IsEnabled
WHERE ""Id"" = @Id;";

        using var connection = CreateConnection();
        var affectedRows = await connection.ExecuteAsync(new CommandDefinition(sql, memberData, cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    public async Task<Member?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT ""Id"", ""Name"", ""Birthday"", ""Address"", ""Phone"", ""RegisterOn"", ""IsEnabled""
FROM ""Members""
WHERE ""Id"" = @Id;";

        using var connection = CreateConnection();
        var memberData = await connection.QuerySingleOrDefaultAsync<MemberDataModel>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        return memberData is null ? null : MapDataToMember(memberData);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
DELETE FROM ""Members""
WHERE ""Id"" = @Id;";

        using var connection = CreateConnection();
        var affectedRows = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    private static MemberDataModel MapMemberToData(Member member)
    {
        return new MemberDataModel
        {
            Id = member.Id.Value,
            Name = member.Name,
            Birthday = member.Birthday,
            Address = member.Address.Value,
            Phone = member.Phone.Value,
            RegisterOn = member.RegisterOn,
            IsEnabled = member.IsEnabled
        };
    }

    private static Member MapDataToMember(MemberDataModel data)
    {
        return new Member
        {
            Id = MemberId.From(data.Id),
            Name = data.Name,
            Birthday = data.Birthday,
            Address = new Address(data.Address),
            Phone = new Phone(data.Phone),
            RegisterOn = data.RegisterOn,
            IsEnabled = data.IsEnabled
        };
    }

    /// <summary>
    /// Internal DTO used for Dapper mapping to database records.
    /// </summary>
    private class MemberDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Birthday { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTimeOffset RegisterOn { get; set; }
        public bool IsEnabled { get; set; }
    }
}
