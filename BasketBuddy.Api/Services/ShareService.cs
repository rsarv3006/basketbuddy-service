using BasketBuddy.Api.Dto;
using BasketBuddy.Core;
using BasketBuddy.Data;

namespace BasketBuddy.Api.Services;

public class ShareService
{
   private readonly ShareRepository shareRepo;
      
   public ShareService(ShareRepository shareRepository)
   {
      shareRepo = shareRepository;
   }
   
   public async Task<Share> createShare(ShareCreateDto dto)
   {
      ArgumentNullException.ThrowIfNull(dto.GetData());
      
      return await shareRepo.CreateShare(dto.GetData()); 
   }
}