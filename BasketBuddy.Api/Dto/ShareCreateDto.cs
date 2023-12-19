using System.Text.Json;
using Microsoft.AspNetCore.Antiforgery;

namespace BasketBuddy.Api.Dto;

public struct ShareCreateDto
{
        public JsonDocument Data { get; set; }
}