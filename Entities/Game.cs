// Indentazione: ctrl+. -> Convert to file scoped namespace 
namespace Gamestore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    // public string Name { get; set; } = "some default value";
    // public string Name { get; set; } = string.Empty;
    // public string? Name { get; set; }
    // Altrimenti aggiungere required
    public required string Name { get; set; }
    public required string Genre { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required string ImageURI { get; set; }
}