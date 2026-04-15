using System.Data;
using application.Interfaces;
using Dapper;
using domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly string _connectionString;

    public MemberRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }

    public async Task<Guid> CreateAsync(MemberDataModel member, CancellationToken cancellationToken = default)
    {
        const string sql = @"
INSERT INTO Members (Name, Birthday, Address, Phone, RegisterOn, IsEnabled)
OUTPUT INSERTED.Id
VALUES (@Name, @Birthday, @Address, @Phone, @RegisterOn, @IsEnabled);";

        using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<Guid>(new CommandDefinition(sql, member, cancellationToken: cancellationToken));
    }

    public async Task<bool> UpdateAsync(MemberDataModel member, CancellationToken cancellationToken = default)
    {
        const string sql = @"
UPDATE Members
SET Name = @Name,
    Birthday = @Birthday,
    Address = @Address,
    Phone = @Phone,
    RegisterOn = @RegisterOn,
    IsEnabled = @IsEnabled
WHERE Id = @Id;";

        using var connection = CreateConnection();
        var affectedRows = await connection.ExecuteAsync(new CommandDefinition(sql, member, cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    public async Task<MemberDataModel?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT Id, Name, Birthday, Address, Phone, RegisterOn, IsEnabled
FROM Members
WHERE Id = @Id;";

        using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<MemberDataModel>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
DELETE FROM Members
WHERE Id = @Id;";

        using var connection = CreateConnection();
        var affectedRows = await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        return affectedRows > 0;
    }

    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
