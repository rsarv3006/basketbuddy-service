using BasketBuddy.Core;
using Microsoft.EntityFrameworkCore;

namespace BasketBuddy.Data;

public class BasketBuddyContext : DbContext
{
    public DbSet<Share> Shares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=postgres;Username=admin;Password=admin");
    }
    
    public BasketBuddyContext(DbContextOptions<BasketBuddyContext> options) 
        : base(options)
    {
    }
}