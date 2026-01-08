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
}
