using Gamestore.Api.Endpoints;
using Gamestore.Api.Entities;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Dopo aver creato Class C# -> GameEndpoints.cs, sposto tutti i endpoint in quella classe
// Importo il namespace Gamestore.Api.Endpoints
app.MapGamesEndpoints();

// Sistemo il progetto per dargli una struttura adatta a repository pattern
// Creo cartella Repositories -> InMemGameRepository.cs e ci copio la lista di giochi da GameEndpoints

app.Run();
