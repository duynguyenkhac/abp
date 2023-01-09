using System;
using System.Threading.Tasks;
using MySqlConnector;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.EntityFrameworkCore.ConnectionStrings;

[ExposeServices(typeof(IAbpConnectionStringChecker))]
public class AbpEfCoreMySqlConnectionStringChecker : IAbpConnectionStringChecker, ITransientDependency
{
    public virtual async Task<AbpConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new AbpConnectionStringCheckResult();
        var connString = new MySqlConnectionStringBuilder(connectionString)
        {
            ConnectionLifeTime = 1
        };

        var oldDatabaseName = connString.Database;
        connString.Database = "mysql";

        try
        {
            await using var conn = new MySqlConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
            await conn.ChangeDatabaseAsync(oldDatabaseName);
            await conn.CloseAsync();
            result.DatabaseExists = true;
            return result;
        }
        catch (Exception e)
        {
            return result;
        }
    }
}
