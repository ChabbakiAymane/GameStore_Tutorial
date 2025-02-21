using System.Reflection;
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

    // L'entità che andremo a gestire è Game
    public DbSet<Game> Games => Set<Game>();
    
    // Faccio override di OnModelCreating() per configurare Entity Type Framework con la precisione impostata
    // in Data/Configurations/GameConfiguration.cs
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ogni volta che il modello viene creato come parte della Migration
        // il Context specifica al tool della Migration (ef) che deve applicare la configurazione
        // definita in GameConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}