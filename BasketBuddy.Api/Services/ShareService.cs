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
      ArgumentNullException.ThrowIfNull(dto.GetData());
      
      return await _shareRepository.CreateShare(dto.GetData()); 
   }
}