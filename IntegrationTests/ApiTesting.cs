using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto;
using Application.Entities;
using FluentAssertions;
using Xunit;

namespace IntegrationTests;

public class ApiTesting : IClassFixture<AppDbFixture<Program>>
{
    private readonly AppDbFixture<Program> _dbFixture;

    public ApiTesting(AppDbFixture<Program> dbFixture)
    {
        _dbFixture = dbFixture;
    }

    [Fact]
    public async Task AddAndGet_Success()
    {
        var client = _dbFixture.CreateClient();
        var carContent = new CarDto
        {
            Name = "abc",
            Brand = "xyz",
            Horsepower = 100,
            EngineCapacity = 1.2
        };
        
        // act
        var responseAdd = await client.PostAsJsonAsync("api/car", carContent);
        var addedCarId = await responseAdd.Content.ReadAsStringAsync(CancellationToken.None);
        var resultAllCars = await client.GetFromJsonAsync<List<CarEntity>>("api/cars");
        
        // assert
        resultAllCars.Should()
            .NotBeNull()
            .And.NotBeEmpty()
            .And.ContainSingle(n => n.Id.ToString().Equals(addedCarId));
    }
}