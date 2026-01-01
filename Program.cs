using GameStore.Api;

const string FetchGameEndpoint = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (1, "Grand Theft Auto V", "Action-adventure", 29.99m, new DateOnly(2013, 9, 17)),
    new (2, "Grand Theft Auto IV", "Action-adventure", 19.99m, new DateOnly(2009, 9, 17)),
    new (1, "Grand Theft Auto III", "Action-adventure", 9.99m, new DateOnly(2003, 9, 17)),

];

app.MapGet("/game/all", () => games);

app.MapGet("/game/{id}", (int id) =>
{
   return games.Find(g => g.Id == id); 
}).WithName(FetchGameEndpoint);

app.MapPost("/game/add", (CreateGameDto newGame) =>
{
    GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre, 
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(FetchGameEndpoint, new {id = game.Id}, game);
});

app.MapPut("/game/update/{id}" , (int id, UpdateGameDto updateGame) =>
{
    var index = games.FindIndex(game => game.Id == id);
    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );

    return Results.NoContent();
});

app.MapDelete("/game/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();