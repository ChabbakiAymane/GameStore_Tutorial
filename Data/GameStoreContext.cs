using Gamestore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Api.Data;

// Per poter usare il Enity Framework Core, devo creare una classe che estende DbContext
// DbContext è la classe principale di Entity Framework Core, rappresenta il contesto di un DB
// e consente di eseguire query e salvare dati
public class GameStoreContext : DbContext
{
    // Permette alla classe GameStoreContext di ottenere tutti i dati necessari su come connettersi
    // al Database (compreso la connectionString data dal Secret Manager)
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
}