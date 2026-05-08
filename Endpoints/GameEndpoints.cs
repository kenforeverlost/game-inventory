using GameInventory.Data;
using GameInventory.Dtos;
using GameInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace GameInventory.Endpoints;

public static class GameEndpoints
{
    // Example Games:
    // {
    //     "Name":"Street Fighter II",
    //     "GenreId": 1,
    //     "Price": 19.99,
    //     "ReleaseDate": "1992-07-15"
    // }
    // {
    //     "Name":"Final Fantasy VII Rebirth",
    //     "GenreId": 2,
    //     "Price": 69.99,
    //     "ReleaseDate": "2024-02-29"
    // }
    // {
    //     "Name":"Astro Bot",
    //     "GenreId": 3,
    //     "Price": 39.99,
    //     "ReleaseDate": "2024-09-06"
    // }

    public static void MapGamesEndpoints(this WebApplication app)
    {
        const string GetGameEndpoint = "GetName";

        var group = app.MapGroup("/games");

        group.MapGet(
            "/",
            async (GameStoreContext dbContext) =>
            {
                var games = await dbContext
                    .Games.Include(game => game.Genre)
                    .Select(game => new GameSummaryDto(
                        game.Id,
                        game.Name,
                        game.Genre!.Name,
                        game.Price,
                        game.ReleaseDate
                    ))
                    .AsNoTracking()
                    .ToListAsync();

                return games;
            }
        );
        group
            .MapGet(
                "/{id}",
                async (int id, GameStoreContext dbContext) =>
                {
                    var game = await dbContext.Games.FindAsync(id);

                    if (game is null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        GameDetailsDto gameDto = new(
                            game.Id,
                            game.Name,
                            game.GenreId,
                            game.Price,
                            game.ReleaseDate
                        );

                        return Results.Ok(gameDto);
                    }
                }
            )
            .WithName(GetGameEndpoint);

        group.MapPost(
            "/",
            async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = new()
                {
                    Name = newGame.Name,
                    GenreId = newGame.GenreId,
                    Price = newGame.Price,
                    ReleaseDate = newGame.ReleaseDate,
                };

                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();

                GameDetailsDto gameDto = new(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                );

                return Results.CreatedAtRoute(GetGameEndpoint, new { id = gameDto.Id }, gameDto);
            }
        );

        group.MapPut(
            "/{id}",
            async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGame.Name;
                existingGame.GenreId = updatedGame.GenreId;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        group.MapDelete(
            "/{id}",
            async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

                return Results.NoContent();
            }
        );
    }
}
