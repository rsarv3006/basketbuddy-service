using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace BasketBuddy.Data;

public class DataContext : DbContext
{
    public DbSet<Share> Shares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=postgres;Username=admin;Password=admin");
    }
    
    public DataContext(DbContextOptions<DataContext> options) 
        : base(options)
    {
    }
}

[Table("Shares")]
[Index(nameof(ShareCode), IsUnique = true)]
public class Share 
{
    public Share(JsonContent data)
    {
        Data = data;
        ShareCode = GenerateShareCode();
    }
    
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime Expiration { get; set; }

    [Required] 
    public JsonContent Data { get; set; }

    [Required]
    [MaxLength(6)]
    public string ShareCode { get; set; }

    private string GenerateShareCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var rng = new Random();
        string code;
        
        try
        {
            code = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[rng.Next(s.Length)]).ToArray());
        }
        catch (Exception ex) when (ex.Message.Contains("unique constraint"))
        {
            code = GenerateShareCode(); 
        }

        return code;
    }
}