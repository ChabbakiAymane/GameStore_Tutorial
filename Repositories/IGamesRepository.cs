using Gamestore.Api.Entities;

namespace Gamestore.Api.Repositories;

public interface IGamesRepository
{
    void Create(Game newGame);
    void Delete(int id);
    Game? Get(int id);
    IEnumerable<Game> GetAll();
    void Update(Game updatedGame);
}
