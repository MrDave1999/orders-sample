using DotEnv.Core;
using Infrastructure;
using MySqlConnector;

namespace IntegrationTests;

public class TestBase
{
    private CustomWebApplicationFactory _applicationFactory;
    protected CustomWebApplicationFactory ApplicationFactory => _applicationFactory;

    /// <summary>
    /// Initializes the web application before starting tests for a class.
    /// </summary>
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
        => _applicationFactory = new CustomWebApplicationFactory();

    /// <summary>
    /// Frees resources upon completion of all tests in a class.
    /// </summary>
    [OneTimeTearDown]
    public void RunAfterAnyTests()
        => _applicationFactory.Dispose();

    /// <summary>
    /// Creates the database before starting a test and deletes data from each table.
    /// </summary>
    /// <remarks>The database is only created if it does not exist.</remarks>
    [SetUp]
    public async Task Init()
    {
        using var scope = ApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        context.Database.EnsureCreated();
        await Cleanup();
    }

    private static async Task Cleanup()
    {
        using var connection = new MySqlConnection(EnvReader.Instance["DB_CONNECTION_STRING"]);
        await connection.OpenAsync();
        var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            SchemasToInclude = new[]
            {
                EnvReader.Instance["DB_DATABASE"]
            },
            DbAdapter = DbAdapter.MySql,
            WithReseed = true
        });
        await respawner.ResetAsync(connection);
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = ApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        context.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity[]> AddRangeAsync<TEntity>(TEntity[] entities) where TEntity : class
    {
        using var scope = ApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        context.AddRange(entities);
        await context.SaveChangesAsync();
        return entities;
    }

    public async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = ApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        return await context.FindAsync<TEntity>(keyValues);
    }
}
