using Unicam.Paradigmi.GestoreMusicale.Application.Extensions;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Extensions;
using Unicam.Paradigmi.GestoreMusicale.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
