using System.Threading.Tasks;
using Gamestore.Api.Entities;

namespace Gamestore.Api.Repositories;

// Dopo aver generato l'interfaccia, la classe InMemGameRepository implementa l'interfaccia IInMemGameRepository
// Sposto l'interfaccia in un file separato (ctrl+. -> Move type to IGamesRepository.cs)
public class InMemGameRepository : IGamesRepository
{
    // Per dargli una struttura da dependency injection, creo una view estraendo l'interfaccia
    // su InMemGameRepository -> ctrl+. -> Extract Interface

    // Dichiarazione della lista di giochi in-memory
    private readonly List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateTime(1991, 2, 1),
            ImageURI = "https://placehold.co/100",
        },
        new Game()
        {
            Id = 2,
            Name = "Final Fantasy VII",
            Genre = "Fighting",
            Price = 39.99M,
            ReleaseDate = new DateTime(2010, 9, 3),
            ImageURI = "https://placehold.co/100",
        },
        new Game()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 29.99M,
            ReleaseDate = new DateTime(2022, 9, 27),
            ImageURI = "https://placehold.co/100",
        },
    };

    // Definisco i metodi di InMemGameRepository
    // @ IEnumerable<Game> GetAll():
    //  - Restituisce la lista di tutti i giochi
    //  - IEnumerable<Game> tipo di dato generico iterabile di tipo Game
    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    // @ Game? Get(int id): cerca il gioco con id = {id} (se non presente, restituisce null)
    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(game => game.Id == id));
    }

    // @ void Create(Game game): crea un nuovo gioco e lo aggiunge alla lista
    public async Task CreateAsync(Game newGame)
    {
        newGame.Id = games.Max(game => game.Id) + 1;
        games.Add(newGame);

        await Task.CompletedTask;
    }

    // @ void Update(Game game): aggiorna il gioco con id = {id} con il nuovo gioco
    public async Task UpdateAsync(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;

        await Task.CompletedTask;
    }

    // @ void Delete(int id): elimina il gioco con id = {id} dalla lista
    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);

        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Game>> SearchGenreAsync(string genre)
    {
        return await Task.FromResult(games.Where(game => game.Genre == genre));
    }

    public async Task<IEnumerable<Game>> SearchNameAsync(string title)
    {
        return await Task.FromResult(games.Where(game => game.Name == title));
    }
}
