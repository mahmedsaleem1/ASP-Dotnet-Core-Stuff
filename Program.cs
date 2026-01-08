using GameStore.Api.Data;
using GameStore.Api.Endpoints;

using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddValidation(); // For every Endpoint
// Before creating app
// Registering Database

builder.AddGenreToDb();

var app = builder.Build();

// var secret = Environment.GetEnvironmentVariable("NAME");
// Console.WriteLine(secret);

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();