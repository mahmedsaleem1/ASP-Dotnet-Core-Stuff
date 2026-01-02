using GameStore.Api;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); // For every Endpoint

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();