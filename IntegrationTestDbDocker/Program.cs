using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UsePersistenceConfiguration();

app.MapPost("Test", () =>
{
    return " --- post ---";
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