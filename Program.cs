using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); // For every Endpoint
// Before creatingg app
// Registering Database
var connString = "Data Source=GameStore.db"; // sqlite db // Options
builder.Services.AddSqlite<GameStoreContexts>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();