using System;

namespace GameStore.Api.Endpoints;

// Extension method -> in static class
// static class can have static methods
public static class GamesEndpoints
{
    const string FetchGameEndpoint = "GetGame";
 
    private static List<GameDto> games = [
        new (1, "Grand Theft Auto V", "Action-adventure", 29.99m, new DateOnly(2013, 9, 17)),
        new (2, "Grand Theft Auto IV", "Action-adventure", 19.99m, new DateOnly(2009, 9, 17)),
        new (1, "Grand Theft Auto III", "Action-adventure", 9.99m, new DateOnly(2003, 9, 17)),
    ];

    public static void MapGamesEndpoints(this WebApplication app) {

        app.MapGet("/", () => new { message = "GameStore API", endpoint = "/games/all" });

        var group = app.MapGroup("/game");

        group.MapGet("/all", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(g => g.Id == id); 

            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(FetchGameEndpoint);

        group.MapPost("/add", (CreateGameDto newGame) =>
        {
            // Possible but bad approach
            // if (String.IsNullOrEmpty(newGame.Name))
            // {
            //     return Results.BadRequest("Name is Required");
            // } We will have to apply this for every data object

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

        group.MapPut("/update/{id}" , (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });
    }
}
