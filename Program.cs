using Gamestore.Api.Entities;

const string getGameEndpointName = "GetGameById";

// In-memory list of games
List<Game> games = new()
{
    new Game(){

        Id = 1,
        Name = "Street Fighter II",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991, 2, 1),
        ImageURI = "https://placehold.co/100"
    },
    new Game(){
        Id = 2,
        Name = "Final Fantasy VII",
        Genre = "Fighting",
        Price = 59.99M,
        ReleaseDate = new DateTime(2010, 9, 3),
        ImageURI = "https://placehold.co/100"
    },
    new Game(){
        Id = 3,
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 29.99M,
        ReleaseDate = new DateTime(2022, 9, 27),
        ImageURI = "https://placehold.co/100"
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Utilizzo GROUPS per raggruppare gli ENDPOINTS
// Aggiunta MinimalApis.Extensions, la abilito per il group tramite WithParameterValidation()
var group = app.MapGroup("/games").WithParameterValidation();

// Definizione di ENDPOINTS
// GET /games: Restituisce la lista di tutti i giochi
group.MapGet("/", () => games);

// GET /games/{id}: Restituisce il gioco con id = {id}
group.MapGet("/{id}", (int id) => 
{
    // Cerco il gioco con id = {id} (se non presente, con ? accetto null)
    Game? gametoFind = games.Find(game => game.Id == id);
    if (gametoFind is null)
    {
        // Restituisco risposta REST: 404 Not Found
        return Results.NotFound();
    }
    // Restituisco il gioco trovato
    return Results.Ok(gametoFind);
    // Assegno il nome "GetGameById" all'endpoint
    // Posso usarlo per invocare questo endpoint tramite CreatedAtRoute()
}).WithName(getGameEndpointName);

// POST /games: Crea un nuovo gioco e restituisce URL del gioco creato
group.MapPost("/", (Game GameCreated) => 
{
    // Tra tutti i giochi, trovo id più grande e lo incremento
    GameCreated.Id = games.Max(game => game.Id) + 1;
    games.Add(GameCreated);
    // Restituisco risposta REST: 201 Created
    // Restituisco l'entità restituita dalla chiamata all'endpoint /games/{id}
    return Results.CreatedAtRoute(getGameEndpointName, new { id = GameCreated.Id }, GameCreated);
});

// PUT /games/{id}: Aggiorna il gioco con id = {id}
group.MapPut("/{id}", (int id, Game updatedGame) => 
{
    // Cerco il gioco con id = {id} (se non presente, con '?' accetto null)
    Game? gametoUpdate = games.Find(game => game.Id == id);
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
    // Restituisco risposta REST: 204 No Content
    return Results.NoContent();
});

// DELETE /games/{id}: Elimina il gioco con id = {id}
group.MapDelete("/{id}", (int id) => {
    // Cerco il gioco con id = {id} (se non presente, con '?' accetto null)
    Game? gametoDelete = games.Find(game => game.Id == id);
    if (gametoDelete is not null)
    {
        // Elimino il gioco trovato
        games.Remove(gametoDelete);
    }
    // Restituisco risposta REST: 204 No Content
    // Che trovi il gioco o meno, la risposta è sempre la stessa
    return Results.NoContent();
});

app.Run();
