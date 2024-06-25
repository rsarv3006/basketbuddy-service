using System.Text.Json;
using BasketBuddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketBuddy.Api.Repositories;

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

  public async Task DeleteShare(string shareCode)
  {
    var share = await _context.Shares.FirstOrDefaultAsync(s => s.ShareCode == shareCode);
    if (share == null) throw new Exception("Share not found");

    _context.Shares.Remove(share);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAllExpiredShares()
  {
    var expiredShares = await _context.Shares.Where(s => s.Expiration < DateTime.UtcNow).ToListAsync();
    _context.Shares.RemoveRange(expiredShares);
    await _context.SaveChangesAsync();
  }
}
