using GameInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace GameInventory.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Decimals need precision for left and right of decimal point
        modelBuilder.Entity<Game>().Property(g => g.Price).HasPrecision(18, 2);
    }
}
