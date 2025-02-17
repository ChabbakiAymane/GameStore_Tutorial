using Gamestore.Api.Endpoints;
using Gamestore.Api.Entities;
using Gamestore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Aggiungo servizi al container di dependency injection
// AddScoped: crea un'istanza separata per ogni richiesta HTTP (se aggiungo gioco, non lo vedo in un'altra richiesta)
// AddSingleton: crea un'unica istanza condivisa per tutta l'applicazione (se aggiungo gioco, lo vedo in un'altra richiesta)
builder.Services.AddSingleton<IGamesRepository, InMemGameRepository>();

var app = builder.Build();

// Dopo aver creato Class C# -> GameEndpoints.cs, sposto tutti i endpoint in quella classe
// Importo il namespace Gamestore.Api.Endpoints
// Creo cartella Repositories -> InMemGameRepository.cs e ci copio la lista di giochi da GameEndpoints
// Sistemo il progetto per dargli una struttura adatta a repository pattern
// Aggiorno il progetto dandoli una struttura per dependency injection con il repository pattern
app.MapGamesEndpoints();

app.Run();
