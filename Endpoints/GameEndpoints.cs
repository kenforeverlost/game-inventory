using GameInventory.Data;
using GameInventory.Dtos;
using GameInventory.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GameInventory.Endpoints;

public static class GameEndpoints
{
    private static string _getGameEndpoint = "GetName";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", Get);
        group.MapGet("/{id}", GetById).WithName(_getGameEndpoint);

        group.MapPost("/", Create);

        group.MapPut("/{id}", Update);

        group.MapDelete("/{id}", Delete);
    }

    private static async Task<Ok<List<GameSummaryDto>>> Get(GameStoreContext dbContext)
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

        return TypedResults.Ok(games);
    }

    private static async Task<Results<Ok<GameDetailsDto>, NotFound>> GetById(
        int id,
        GameStoreContext dbContext
    )
    {
        var game = await dbContext.Games.FindAsync(id);

        if (game is null)
        {
            return TypedResults.NotFound();
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

            return TypedResults.Ok(gameDto);
        }
    }

    private static async Task<CreatedAtRoute<GameDetailsDto>> Create(
        CreateGameDto newGame,
        GameStoreContext dbContext
    )
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

        return TypedResults.CreatedAtRoute(gameDto, _getGameEndpoint, new { id = gameDto.Id });
    }

    private static async Task<Results<Ok, NotFound>> Update(
        int id,
        UpdateGameDto updatedGame,
        GameStoreContext dbContext
    )
    {
        var existingGame = await dbContext.Games.FindAsync(id);

        if (existingGame is null)
        {
            return TypedResults.NotFound();
        }

        existingGame.Name = updatedGame.Name;
        existingGame.GenreId = updatedGame.GenreId;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;

        await dbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }

    private static async Task<Ok> Delete(int id, GameStoreContext dbContext)
    {
        await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

        return TypedResults.Ok();
    }
}
