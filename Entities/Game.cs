// Indentazione: ctrl+. -> Convert to file scoped namespace 
using System.ComponentModel.DataAnnotations;

namespace Gamestore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    /* Dichiarazione della proprietà Name:
        public string Name { get; set; } = "some default value";
        public string Name { get; set; } = string.Empty;
        public string? Name { get; set; }
    */
    // Aggiungo DataAnnotations per imporre vincoli strutturali sui dati
    // Non bastano per imporre i vincoli, devo usare un endpointsFilter: 
    //  - MinimalApis.Extensions (NuGet package)
    //  - dotnet add package MinimalApis.Extensions --version 0.10.0
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required]
    [StringLength(20)]
    public required string Genre { get; set; }
    [Range(0.01, 99.99)]
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    [Url]
    [StringLength(100)]
    public required string ImageURI { get; set; }
}