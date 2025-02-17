using System.ComponentModel.DataAnnotations;

namespace Gamestore.Api.DTOs;

public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageURI
);

public record CreateGameDto(
    [Required][StringLength(50)]    string Name,
    [Required][StringLength(20)]    string Genre,
    [Range(1, 100)]                 decimal Price,
    [DataType(DataType.Date)]       DateTime ReleaseDate,
    [Url][StringLength(100)]        string ImageURI
);

public record UpdateGameDto(
    [Required][StringLength(50)]    string Name,
    [Required][StringLength(20)]    string Genre,
    [Range(1, 100)]                 decimal Price,
    [DataType(DataType.Date)]       DateTime ReleaseDate,
    [Url][StringLength(100)]        string ImageURI
);

// Creo un extension method per convertire Game Entity in GameDto Entity
// Dir Entities -> Creo Classe EntityExtensions.cs