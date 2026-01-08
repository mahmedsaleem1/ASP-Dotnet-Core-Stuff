using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app) // MigrateDb extends WebApplication class
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider
                            .GetRequiredService<GameStoreContexts>();
        
        dbContext.Database.Migrate();
        // created scope, got context and extecuted the migration 
    }
    public static void AddGenreToDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("GameStore"); // sqlite db // Options
        builder.Services.AddSqlite<GameStoreContexts>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Fighting" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Platformer" },
                        new Genre { Name = "Racing" },
                        new Genre { Name = "Sports" }
                    );
                    context.SaveChanges();
                }
            })
        );
    }
}