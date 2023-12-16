using Microsoft.AspNetCore.Antiforgery;

namespace BasketBuddy.Api.Dto;

public struct ShareCreateDto
{
    private JsonContent Data { get; }
    
    public ShareCreateDto(JsonContent data)
    {
        Data = data;
    }
    
    public JsonContent GetData()
    {
        return Data;
    }
}