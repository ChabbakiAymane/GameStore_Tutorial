using Gamestore.Api.Entities;

namespace Gamestore.Api.Repositories;

public interface IGamesRepository
{
    // Aggiungo "Async" al nome dei metodi
    // Cambio il tipo di ritorno in Task
    Task CreateAsync(Game newGame);
    Task DeleteAsync(int id);
    Task<Game?> GetAsync(int id);
    Task<IEnumerable<Game>> GetAllAsync();
    Task UpdateAsync(Game updatedGame);
}
