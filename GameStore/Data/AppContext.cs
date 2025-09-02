using Microsoft.EntityFrameworkCore;
using GameStore.Models;

namespace GameStore.Data;

public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<WishList> WishList { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>()
            .Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()"); // PostgreSQL UUID generator

        modelBuilder.Entity<WishList>()
            .HasOne(w => w.User)
            .WithMany(u => u.WishLists)
            .HasForeignKey(w => w.UserId);

        modelBuilder.Entity<WishList>()
            .HasOne(w => w.Game)
            .WithMany(g => g.WishLists)
            .HasForeignKey(w => w.GameId);
    }
}
