using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public class ApiTesting : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<MsDbFixture>
{
    private readonly WebApplicationFactory<Program> _webFactory;
    private readonly MsDbFixture _dbFactory;

    public ApiTesting(WebApplicationFactory<Program> webFactory, MsDbFixture dbFactory)
    {
        _webFactory = webFactory;
        _dbFactory = dbFactory;
    }

    [Fact]
    public async Task AddTest()
    {
        var client = _webFactory.CreateClient();
        var i = await client.GetAsync("api/cars");
        var c = await i.Content.ReadAsStringAsync(CancellationToken.None);
    }

    [Fact]
    public async Task XyzTest()
    {
        // var container = new ContainerBuilder<MsSqlContainer>()
        //     .ConfigureDatabaseConfiguration("not-important", "not-important", "not-important")
        //     .Build();
        //
        // var actual = container.DockerImageName;
        //
        // var i = actual.Length;
    }
}