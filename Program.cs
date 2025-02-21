using Gamestore.Api.Data;
using Gamestore.Api.Endpoints;
using Gamestore.Api.Entities;
using Gamestore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Aggiungo servizi al container di dependency injection
// AddScoped: crea un'istanza separata per ogni richiesta HTTP (se aggiungo gioco, non lo vedo in un'altra richiesta)
// AddSingleton: crea un'unica istanza condivisa per tutta l'applicazione (se aggiungo gioco, lo vedo in un'altra richiesta)
builder.Services.AddSingleton<IGamesRepository, InMemGameRepository>();

var connString = builder.Configuration.GetConnectionString("GameStoreContext");
// Come fare builder.Services.AddScoped<> solo che AddSqlServer lo fa in automatico
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

// Dopo aver creato Class C# -> GameEndpoints.cs, sposto tutti i endpoint in quella classe
// Importo il namespace Gamestore.Api.Endpoints
// Creo cartella Repositories -> InMemGameRepository.cs e ci copio la lista di giochi da GameEndpoints
// Sistemo il progetto per dargli una struttura adatta a repository pattern
// Aggiorno il progetto dandoli una struttura per dependency injection con il repository pattern
app.MapGamesEndpoints();

// Aggiunto DTO per vincolare il contratto di scambio tra Server e Client ed evitare che
// il Server invii dati che rompono il Client o dati di cui non ha bisogno (es. ID)
// DTO specifica un contratto su come deve avvenire (aspettative/requisiti) lo scambio di dati tra Server e Client

// Aggiungo Entity Framework Core/DbContext
// Utilizziamo O/RM (Object-Relational Mapping), tecnica per convertire dati tra un DB-relazionale e Object Oriented Program
// .NET utilizza come O/RM: Entity Framework Core
// Aggiungo Entity Framework Core da NuGet: terminal -> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// Creo una nuova cartella Data -> GamestoreDbContext.cs

// Aggiungo il GameStoreContext al service provider tramite AddSqlServer
// che ci permette di registrarlo con la connectionString

// I file di migrazione automatizzano l'evoluzione della struttura del database senza dover scrivere manualmente query SQL:
//   - File che registra le modifiche alla struttura di un database in modo programmato e tracciabile.
//   - Permette di versionare il database, mantenendo un registro delle modifiche.
//   - Permette operazione di rollback.
//   - Permette di creare un database vuoto a partire da un modello.
// Migrazione DB (tool da installare per la migrazione):
//  - dotnet tool install --global dotnet-ef (tool per la migrazione)
// NuGet-package che genera automaticamente i file di migrazione per un database:
//  - dotnet add package Microsoft.EntityFrameworkCore.Design



app.Run();
