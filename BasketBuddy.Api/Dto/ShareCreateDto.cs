using System.Text.Json;
using Microsoft.AspNetCore.Antiforgery;

namespace BasketBuddy.Api.Dto;

public struct ShareCreateDto
{
    private JsonDocument Data { get; }
    
    public ShareCreateDto(JsonDocument data)
    {
        Data = data;
    }
    
    public JsonDocument GetData()
    {
        return Data;
    }
}