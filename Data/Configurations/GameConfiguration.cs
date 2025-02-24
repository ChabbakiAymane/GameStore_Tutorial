using Gamestore.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.Api.Data.Configurations;

// Devo implementare l'interfaccia IEntityTypeConfiguration<T> per rendere questa classe una configurazione di Entity Type Framework
public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        // Qua decido la precisione del tipo decimal (0.00 a 99.99)
        builder.Property(game => game.Price).HasPrecision(5, 2);
        // Ora vado su GameStoreContext e aggiungo la configurazione
    }
}
