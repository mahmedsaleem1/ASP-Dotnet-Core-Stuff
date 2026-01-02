using System.ComponentModel.DataAnnotations;

namespace GameStore.Api;

public record CreateGameDto
( // Data Annotation -> Restrictions | Must be activated in Builder
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)]string Genre,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate
);
