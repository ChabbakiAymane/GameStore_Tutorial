using Gamestore.Api.DTOs;
using Gamestore.Api.Entities;
using Gamestore.Api.Repositories;

namespace Gamestore.Api.Endpoints;

// Extension methods sono static, quindi anche la classe la dichiaro static
public static class GamesEndpoints
{
    // Sposto da Program.cs:
    //  - definizione degli endpoint REST (sotto)
    //  - costante del nome dell'endpoint
    const string getGameEndpointName = "GetGameById";

    //  - lista di giochi in-memory (da dichiarare static, poi spostata in InMemGameRepository)

    // Ora il metodo non lo invoco più con 'app' ma con 'routes' prima di usare 'group'
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        // Utilizzo GROUPS per raggruppare gli ENDPOINTS
        // Aggiunta MinimalApis.Extensions, la abilito per il group tramite WithParameterValidation()

        // Una volta creata la classe InMemGameRepository, devo definire l'istanza che la utilizza
        // Dopo aver creato la classe IGamesRepository, tolgo questa definizione
        // InMemGameRepository repository = new();
        // Ora devo injectare la repository all'interno del costruttore di GamesEndpoints

        // Ora cambio tutte i riferimenti a 'games' con 'repository.x()' dove x è la funzione che mi serve
        var group = routes.MapGroup("/games").WithParameterValidation();

        // Definizione di ENDPOINTS
        // GET /games: Restituisce la lista di tutti i giochi
        // group.MapGet("/", () => games);

        // Faccio injection della repository IGamesRepository
        // Una volta creati i metodi di conversione GameEntity in DTO-Entity, posso usare AsDto()
        group.MapGet(
            "/",
            (IGamesRepository repository) =>
                // Prendo tutti i giochi, uno alla volta, e li converto in DTO
                repository.GetAll().Select(game => game.AsDto())
        );

        // GET /games/{id}: Restituisce il gioco con id = {id}
        group
            .MapGet(
                "/{id}",
                (IGamesRepository repository, int id) =>
                {
                    // Cerco il gioco con id = {id} (se non presente, con ? accetto null)
                    // Game? gametoFind = games.Find(game => game.Id == id);
                    Game? gametoFind = repository.Get(id);
                    // Restituisco il gioco come DTO
                    return gametoFind is not null
                        ? Results.Ok(gametoFind.AsDto())
                        : Results.NotFound();
                    /*if (gametoFind is null)
                    {
                        // Restituisco risposta REST: 404 Not Found
                        return Results.NotFound();
                    }
                    // Restituisco il gioco trovato
                    return Results.Ok(gametoFind);*/
                    // Assegno il nome "GetGameById" all'endpoint
                    // Posso usarlo per invocare questo endpoint tramite CreatedAtRoute()
                }
            )
            .WithName(getGameEndpointName);

        // POST /games: Crea un nuovo gioco e restituisce URL del gioco creato
        // Faccio injection della repository IGamesRepository
        // Non passo più Game GameCreated, ma CreateGameDto gameDto
        group.MapPost(
            "/",
            (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                // Devo costruire un Game Entity a partire da un gameDto
                Game GameCreated = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageURI = gameDto.ImageURI,
                };
                // Tra tutti i giochi, trovo id più grande e lo incremento
                // GameCreated.Id = games.Max(game => game.Id) + 1;
                // games.Add(GameCreated);
                repository.Create(GameCreated);
                // Restituisco risposta REST: 201 Created
                // Restituisco l'entità restituita dalla chiamata all'endpoint /games/{id}
                return Results.CreatedAtRoute(
                    getGameEndpointName,
                    new { id = GameCreated.Id },
                    GameCreated
                );
            }
        );

        // PUT /games/{id}: Aggiorna il gioco con id = {id}
        // Faccio injection della repository IGamesRepository
        // Non passo più Game updatedGame, ma UpdateGameDto updatedGame
        group.MapPut(
            "/{id}",
            (IGamesRepository repository, int id, UpdateGameDto updatedGame) =>
            {
                // Cerco il gioco con id = {id} (se non presente, con '?' accetto null)
                // Game? gametoUpdate = games.Find(game => game.Id == id);
                Game? gametoUpdate = repository.Get(id);
                if (gametoUpdate is null)
                {
                    // Restituisco risposta REST: 404 Not Found
                    return Results.NotFound();
                    // Altrimenti posso crearlo qua se non esiste, da gestire con DB per ID corretto
                }
                // Aggiorno il gioco trovato con i nuovi dati
                gametoUpdate.Name = updatedGame.Name;
                gametoUpdate.Genre = updatedGame.Genre;
                gametoUpdate.Price = updatedGame.Price;
                gametoUpdate.ReleaseDate = updatedGame.ReleaseDate;
                gametoUpdate.ImageURI = updatedGame.ImageURI;

                // Aggiorno il gioco all'interno della repository
                repository.Update(gametoUpdate);

                // Restituisco risposta REST: 204 No Content
                return Results.NoContent();
            }
        );

        // DELETE /games/{id}: Elimina il gioco con id = {id}
        // Faccio injection della repository IGamesRepository
        group.MapDelete(
            "/{id}",
            (IGamesRepository repository, int id) =>
            {
                // Cerco il gioco con id = {id} (se non presente, con '?' accetto null)
                //Game? gameToDelete = games.Find(game => game.Id == id);
                Game? gameToDelete = repository.Get(id);
                if (gameToDelete is not null)
                {
                    // Elimino il gioco trovato
                    // games.Remove(gameToDelete);
                    repository.Delete(id);
                }
                // Restituisco risposta REST: 204 No Content
                // Che trovi il gioco o meno, la risposta è sempre la stessa
                return Results.NoContent();
            }
        );
        return group;
    }
}
