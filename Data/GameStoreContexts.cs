using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;
// session bw API and Database
// mapping of our objects with the database tables
// options -> credentials + configration 
// Contexts -> Db Class
// DbSet -> Creates a table from our Genre Model
public class GameStoreContexts(DbContextOptions<GameStoreContexts> options) 
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>(); // Games Table
    public DbSet<Genre> Genres => Set<Genre>(); // Genre Table
}
