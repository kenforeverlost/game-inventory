using System.ComponentModel.DataAnnotations;

namespace GameInventory.Dtos;

public record UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Range(1, 200)] decimal Price,
    DateOnly ReleaseDate
) { }
