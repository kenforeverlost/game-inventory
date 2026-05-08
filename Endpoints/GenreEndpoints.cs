using GameInventory.Data;
using GameInventory.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameInventory.Endpoints;

public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        group.MapGet(
            "/",
            async (GameStoreContext dbContext) =>
            {
                var genres = await dbContext
                    .Genres.Select(genre => new GenreDto(genre.Id, genre.Name))
                    .AsNoTracking()
                    .ToListAsync();

                return genres;
            }
        );
    }
};
