using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); // For every Endpoint
// Before creating app
// Registering Database

builder.AddGenreToDb();

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();