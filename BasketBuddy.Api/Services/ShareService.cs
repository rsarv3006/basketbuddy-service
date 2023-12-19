using BasketBuddy.Api.Dto;
using BasketBuddy.Core.Models;

namespace BasketBuddy.Api.Services;

public class ShareService
{
   private readonly ShareRepository _shareRepository;
      
   public ShareService(ShareRepository shareRepository)
   {
      _shareRepository = shareRepository;
   }
   
   public async Task<Share> CreateShare(ShareCreateDto dto)
   {
      ArgumentNullException.ThrowIfNull(dto.Data);
      return await _shareRepository.CreateShare(dto.Data);
   }
   
   public async Task<Share> GetShare(string shareCode)
   {
      ArgumentNullException.ThrowIfNull(shareCode);
      var share = await _shareRepository.GetShare(shareCode);

      if (share.Expiration < DateTime.UtcNow)
      {
         await _shareRepository.DeleteShare(shareCode);
         throw new Exception("Share has expired");
      }

      await _shareRepository.DeleteShare(shareCode);
      return share;
   }
}