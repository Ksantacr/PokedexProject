using Microsoft.EntityFrameworkCore;
using PokedexProject.API.Entities;
using PokedexProject.API.Services;

namespace PokedexProject.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();
        builder.Services.AddDbContext<PokemonDbContext>(options =>
            options.UseMySQL(builder.Configuration.GetConnectionString("PokemonConnection")));
        //name=ConnectionStrings:DefaultConnection


        builder.Services.AddTransient<IPokedexService, PokedexService>(); //<IPokedexService, PokedexService>();
        //builder.Services.AddSingleton<IPokedexService, PokedexService>(); 
        builder.Services.AddHttpClient();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}