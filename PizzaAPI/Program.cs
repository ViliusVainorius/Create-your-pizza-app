using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using PizzaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PizzaAPIDbContext>(options => options.UseInMemoryDatabase("PizzasDb"));
builder.Services.AddDbContext<SizeAPIDbContext>(options => options.UseInMemoryDatabase("SizesDb"));
builder.Services.AddDbContext<ToppingAPIDbContext>(options => options.UseInMemoryDatabase("ToppingsDb"));
builder.Services.AddDbContext<PizzaOrderAPIDbContext>(options => options.UseInMemoryDatabase("PizzaOrdersDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins"); // to prevent CORS policy and be able to fetch data

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
