using Microsoft.EntityFrameworkCore;

namespace PokedexProject.API.Entities;

public class PokemonDbContext : DbContext
{
    public PokemonDbContext(DbContextOptions<PokemonDbContext> options)
        : base(options)
    {
    }
    public DbSet<Pokemon> Pokemons { get; set; }
 
}