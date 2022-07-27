using Application;
using Application.Dto;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UsePersistenceConfiguration();

app.MapPost("Test", async (
    [FromForm] CarDto carDto,
    [FromServices] ICarService car, 
    CancellationToken cancellationToken) =>
{
    var result = await car.Add(
        carDto.Name,
        carDto.Brand,
        carDto.Horsepower,
        carDto.EngineCapacity,
        cancellationToken);
    return $" --- post --- {result}";
});
app.MapGet("Test", () =>
{
    return "-- Test --";
});
app.MapDelete("Test", () =>
{
    return " --- delete ---";
});

app.Run();