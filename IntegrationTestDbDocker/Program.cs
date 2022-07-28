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

app.MapPost("Car", async (
    [FromForm] CarDto carDto,
    [FromServices] ICarService carService, 
    CancellationToken cancellationToken) =>
{
    var result = await carService.Add(
        carDto.Name,
        carDto.Brand,
        carDto.Horsepower,
        carDto.EngineCapacity,
        cancellationToken);
    return $" Added new car: '{result}'";
});
app.MapGet("Cars", async ([FromServices] ICarService carService, CancellationToken cancellationToken) =>
{
    var result = await carService.GetAll(cancellationToken);
    return result;
});
app.MapDelete("Car", async (
    [FromBody] Guid carId,
    [FromServices] ICarService carService,
    CancellationToken cancellationToken) =>
{
    var resutl = await carService.Remove(carId, cancellationToken);
    return resutl;
});

app.Run();