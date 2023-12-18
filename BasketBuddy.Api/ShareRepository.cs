using System.Text.Json;
using BasketBuddy.Api;
using BasketBuddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketBuddy.Api;

public class ShareRepository
{
    private readonly BasketBuddyContext _context;
    
    public ShareRepository(BasketBuddyContext context)
    {
        _context = context;
    }
    
    public async Task<Share> CreateShare(JsonDocument data)
    {
        var share = new Share(data);
        await _context.Shares.AddAsync(share);
        await _context.SaveChangesAsync();
        return share;
    }
    
    public async Task<Share> GetShare(string shareCode)
    {
        var share = await _context.Shares.FirstOrDefaultAsync(s => s.ShareCode == shareCode);
        if (share == null) throw new Exception("Share not found");
        
        return share;
    }
    
}