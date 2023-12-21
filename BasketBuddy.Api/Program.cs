using System.Text;
using BasketBuddy.Api;
using BasketBuddy.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var symmetricKey = Encoding.ASCII.GetBytes("supersecretkeysupersecretkeysupersecretkey");

Console.WriteLine(TokenHelpers.GenerateToken());

var builder = WebApplication.CreateBuilder(args);

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
            IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
            ValidateIssuer = false,
            ValidateAudience = false 
        };
    });

builder.Services.AddControllers();
builder.Services.AddScoped<ShareRepository>();
builder.Services.AddScoped<ShareService>();

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