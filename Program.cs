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

// Aggiunto DTO per vincolare il contratto di scambio tra Server e Client ed evitare che
// il Server invii dati che rompono il Client o dati di cui non ha bisogno (es. ID)
// DTO specifica un contratto su come deve avvenire (aspettative/requisiti) lo scambio di dati tra Server e Client

// Ora utilizzo Docker per creare un'immagine dell'applicazione per il link al database SQL Server
// Devo installare Docker Desktop (https://docs.docker.com/get-started/get-docker/)
// Docker SQL Server image: https://hub.docker.com/r/microsoft/mssql-server
// $sa_password="Pass(w)ord1" -> password per l'utente SA inserita nel terminale
// Eseguo il comando docker run [vedi README.md] per creare il container SQL Server
// Mi connetto al SQL Server tramite l'estensione SQL Server di Visual Studio Code (add connection, ecc...)
// Non ha senso mettere qua i parametri di connessione al DB perchè cambiano, meglio usare .net configuration in appsettings.json:
//   "ConnectionStrings:{
//      "GameStoreContext":"Server=localhost;
//      Database=GameStore;
//      User Id=sa;
//      Password=PASSWORD-GOES-HERE; 
//      TrustServerCertificate=True"
//   }"
var connString = builder.Configuration.GetConnectionString("GameStoreContext");

// Ora per la gestione delle password andiamo ad utilizzare .NET Secret Manager
// Secret Management:
// - Permette di gestire in modo sicuro le password e le chiavi di accesso
// - terminal -> dotnet user-secrets init -> inizializza il file di configurazione per le secret
// - in Gamestore.Api.csproj ottengo il UserSecretsId
// Da terminale, imposto il valore di $sa_password="Pass(w)ord1" ed eseguo il comando dotnet user-secrets set [vedi README.md]
// Con dotnet user-secrets list: posso vedere tutte le secret impostate
// Una volta fatto, posso eliminare la stringa di connessione dal file appsettings.json


app.Run();
