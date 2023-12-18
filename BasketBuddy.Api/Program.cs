using BasketBuddy.Api;
using BasketBuddy.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BasketBuddyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));

builder.Services.AddControllers();
builder.Services.AddScoped<ShareRepository>();
builder.Services.AddScoped<ShareService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRouting();
    
    app.UseEndpoints(endpoints =>  
    {
        endpoints.MapControllers(); 
    });
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();