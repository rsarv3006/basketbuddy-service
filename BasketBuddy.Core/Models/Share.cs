using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace BasketBuddy.Core.Models;

[Table("Shares")]
[Index(nameof(ShareCode), IsUnique = true)]
public class Share 
{
    public Share(JsonDocument data)
    {
        Data = data;
        ShareCode = GenerateShareCode();
        CreatedAt = DateTime.UtcNow;
        Expiration = CreatedAt.AddMinutes(15); 
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public DateTime Expiration { get; set; }

    [Required] 
    public JsonDocument Data { get; set; }

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