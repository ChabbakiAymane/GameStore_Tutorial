using Gamestore.Api.Data;
using Gamestore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Api.Repositories;

// Deve implementare l'interfaccia IGamesRepository
public class EntityFrameworkGamesRepository : IGamesRepository
{
    // Dichiaro l'istanza del DBContext
    private readonly GameStoreContext dbContext;

    // Dependency Injection di GameStoreContext e lo assegna al local dbContext
    public EntityFrameworkGamesRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // Ora implemento i metodi dell'interfaccia IGamesRepository
    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        //throw new NotImplementedException();
        //return dbContext.Games.AsNoTracking().ToList();
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        //throw new NotImplementedException();
        return await dbContext.Games.FindAsync(id);
    }

    public async Task CreateAsync(Game newGame)
    {
        //throw new NotImplementedException();
        // Dice di tenere traccia della nuova entità, nessun codice SQL è eseguito
        dbContext.Games.Add(newGame);
        // Salva le modifiche apportate al database tramite SQL
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game updatedGame)
    {
        //throw new NotImplementedException();
        dbContext.Games.Update(updatedGame);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        //throw new NotImplementedException();
        // Batch delete, efficiente per la gestione della memoria
        await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
    }
}
