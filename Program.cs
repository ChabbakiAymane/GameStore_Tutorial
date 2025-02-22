using Gamestore.Api.Data;
using Gamestore.Api.Endpoints;
using Gamestore.Api.Entities;
using Gamestore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

/*  
# Shortcut Visual Studio Code:
    - Ctrl + b -> Mostra/Nasconde barra laterale Explorer
    - Ctrl + q -> Mostra/Nasconde barra di ricerca
    - Ctrl + Shift + ` -> Apri terminale integrato
    - Ctrl + Shift + p -> Mostra/Nasconde barra comandi

# Git Commands:
    - git rm -r --cached obj/ rimuoviamo la cartella obj (compresi file e sottocartelle) dalla repository
    - git stash: nasconde le modifiche non committate
    - git stash pop: riapplica le modifiche nascoste
    - git reset --hard: cancella tutte le modifiche non committate
    - git clean -fd: rimuove i file non tracciati e le cartelle vuote
    - git clean -f -x -d: rimuove i file non tracciati, le cartelle vuote e i file ignorati (forzando)
    - git restore --staged --worktree . : pristinerà tutti i file che sono stati rimossi in precedenza
    - git reset HEAD~: Annulla l'ultima commit
    - git checkout HEAD Program.cs: ripristina il file Program.cs allo stato precedente al commit
*/

var builder = WebApplication.CreateBuilder(args);

// Aggiungo servizi al container di dependency injection
// AddScoped: crea un'istanza separata per ogni richiesta HTTP (se aggiungo gioco, non lo vedo in un'altra richiesta)
// AddSingleton: crea un'unica istanza condivisa per tutta l'applicazione (se aggiungo gioco, lo vedo in un'altra richiesta)
builder.Services.AddSingleton<IGamesRepository, InMemGameRepository>();

var connString = builder.Configuration.GetConnectionString("GameStoreContext");
// Come fare builder.Services.AddScoped<> solo che AddSqlServer lo fa in automatico
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

/* Migrationi automatiche al momento dello startup dell'applicazione
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    dbContext.Database.Migrate();
}*/

// Meglio creare una classe per gestire le Migration (Data -> DataExtensions.cs)
app.Services.InitializeDb();

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
// Per generare la prima Migration:
//  - dotnet ef migrations add InitialCreate --output-dir Data\Migrations

// Generata la Migration, ora per risolvere il problema della precisione del tipo decimal
// Usiamo Entity Type Configuration per configurare il tipo decimal
// Creo una cartella Data -> Configurations -> GameConfiguration.cs
// Implemento l'interfaccia IEntityTypeConfiguration<T> per rendere la classe una configurazione di Entity Type Framework
// Vado su GameStoreContext e aggiungo la configurazione

// Applico la Migration con un Entity Framework tool:
// - dotnet ef database update 

// Prima devo abilitare il Secret Manager per la connessione al DB:
// - dotnet user-secrets init
// Genero la connectionString:
// - dotnet user-secrets set ..."
// Poi attivo SQL Server tramite Docker e mi collego:
// - docker run ...
// Faccio la Migration iniziale
// - dotnet ef migrations add InitialCreate --output-dir Data\Migrations
// Eseguo la Migration:
// - dotnet ef database update
// 

// Ora impostiamo in modo automatico la Migration al momento dello startup dell'applicazione

app.Run();
