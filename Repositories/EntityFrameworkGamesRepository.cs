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
    public IEnumerable<Game> GetAll()
    {
        //throw new NotImplementedException();
        return dbContext.Games.AsNoTracking().ToList();
    }

    public Game? Get(int id)
    {
        //throw new NotImplementedException();
        return dbContext.Games.Find(id);
    }

    public void Create(Game newGame)
    {
        //throw new NotImplementedException();
        // Dice di tenere traccia della nuova entità, nessun codice SQL è eseguito
        dbContext.Games.Add(newGame);
        // Salva le modifiche apportate al database tramite SQL
        dbContext.SaveChanges();
    }

    public void Update(Game updatedGame)
    {
        //throw new NotImplementedException();
        dbContext.Games.Update(updatedGame);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        //throw new NotImplementedException();
        // Batch delete, efficiente per la gestione della memoria
        dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
    }
}
