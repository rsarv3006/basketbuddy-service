using BasketBuddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketBuddy.Api;

public class BasketBuddyContext : DbContext
{
    public DbSet<Share> Shares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=basketbuddy;Username=admin;Password=admin");
    }
    
    public BasketBuddyContext(DbContextOptions<BasketBuddyContext> options) 
        : base(options)
    {
    }
}