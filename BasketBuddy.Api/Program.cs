using System.Text;
using ApiAlerts.Common.Configurations;
using ApiAlerts.Common.Services;
using ApiUtilities.Common.Handlers;
using ApiUtilities.Common.Interfaces;
using BasketBuddy.Api;
using BasketBuddy.Api.Services;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddDotNetEnv(".env", LoadOptions.TraversePath());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BasketBuddyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET"))),
        ValidateIssuer = false,
        ValidateAudience = false
      };
    });

Console.WriteLine(TokenHelpers.GenerateToken(Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET"))));

builder.Services.AddControllers();
builder.Services.AddScoped<ShareRepository>();
builder.Services.AddScoped<ShareService>();

builder.Services.AddSingleton<IApiConfig, ApiConfig>();
builder.Services.AddScoped<IRequestHandler, RequestHandler>();
builder.Services.AddScoped<AlertService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseRouting();

  app.UseAuthorization();

  app.UseEndpoints(endpoints =>
  {
    endpoints.MapControllers();
  });

  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();
