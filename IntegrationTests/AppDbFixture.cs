using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestContainers.Container.Abstractions.Hosting;
using TestContainers.Container.Database.Hosting;
using TestContainers.Container.Database.MsSql;
using Xunit;

namespace IntegrationTests;

public class AppDbFixture<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
    where TStartup: class
{
    public MsSqlContainer Container { get; }
    public string Username => "sa";
    public string Password => "Abcd1234!";

    public AppDbFixture() : base()
    {
        Container = new ContainerBuilder<MsSqlContainer>()
            .ConfigureDockerImageName("mcr.microsoft.com/mssql/server:2017-latest-ubuntu")
            .ConfigureDatabaseConfiguration("not-used", Password, "not-used")
            .ConfigureLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            })
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var port = Container.GetMappedPort(Container.ExposedPorts[0]);
            var descriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            services.Remove(descriptor);
            services.AddDbContext<ApplicationDbContext>((options, context) =>
            {
                context.UseSqlServer($"Server=127.0.0.1,{port}; Database=IntegrationTestDbDocker; User Id={Username}; Password={Password};");
            });

        });
    }
    
    public async Task InitializeAsync()
        => await Container.StartAsync();

    public new async Task DisposeAsync()
        => await Container.StopAsync();
}