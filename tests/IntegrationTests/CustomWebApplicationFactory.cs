using DotEnv.Core;

namespace IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        new EnvLoader()
            .AddEnvFile("test.env")
            .EnableFileNotFoundException()
            .Load();
    }
}
