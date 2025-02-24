using Gamestore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        // Rendo la chiamata asincrona
        await dbContext.Database.MigrateAsync();
    }

    // Creo nuovo metodo che si occupa della registrazione del repository context
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("GameStoreContext");
        services
            .AddSqlServer<GameStoreContext>(connString)
            .AddScoped<IGamesRepository, EntityFrameworkGamesRepository>();
        return services;
    }
}
