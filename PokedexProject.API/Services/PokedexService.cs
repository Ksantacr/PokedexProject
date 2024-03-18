using Newtonsoft.Json;
using PokedexProject.API.Dtos;
using PokedexProject.API.Entities;
using PokedexProject.API.Models;

namespace PokedexProject.API.Services;

public interface IPokedexService
{
    Task<IList<PokemonResult>> getPokemons();
    Task<PokemonResult> getPokemon(long id);
    Task<PokemonResult> createPokemon(PokemonDto pokemonDto);
    Task<PokemonResult> updatePokemon(int id, PokemonResult pokemonResultUpdated);
    Task deletePokemon(int id);
    Task seedPokemons();
}

public class PokedexService : IPokedexService
{
    private readonly HttpClient _http;
    private readonly PokemonDbContext _dbContext;
    private IList<PokemonResult> _pokemons;

    public PokedexService(HttpClient http, PokemonDbContext dbContext)
    {
        _http = http;
        _dbContext = dbContext;
        _pokemons = new List<PokemonResult>();
    }

    public async Task<IList<PokemonResult>> getPokemons()
    {
        var result = await _http.GetAsync("https://pokeapi.co/api/v2/pokemon");
        //var result2  = await _http.GetFromJsonAsync<PokemonResultAPI>("https://pokeapi.co/api/v2/pokemon");

        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<PokemonResultAPI>(json);
            return jsonResult.results;
        }

        return new List<PokemonResult>();
    }

    public async Task<PokemonResult> getPokemon(long id)
    {
        var result = await _http.GetFromJsonAsync<PokemonById>($"https://pokeapi.co/api/v2/pokemon/{id}");

        //throw new Exception("server exception");

        return new PokemonResult { id = result.Id, name = result.Name, url = $"https://pokeapi.co/api/v2/pokemon/{id}" };
    }

    public Task<PokemonResult> createPokemon(PokemonDto pokemonDto)
    {
        var pokemon = new Pokemon { name = pokemonDto.name, url = $"https://pokeapi.co/api/v2/pokemon/"};
        _dbContext.Pokemons.Add(pokemon);
        _dbContext.SaveChanges();

        
        //_pokemons.Add(pokemon);
        return Task.FromResult<PokemonResult>(new PokemonResult { id = pokemon.id, name = pokemon.name, url = pokemon.url});
    }

    public Task<PokemonResult> updatePokemon(int id, PokemonResult pokemonResultUpdated)
    {
        throw new NotImplementedException();
    }

    public Task deletePokemon(int id)
    {
        throw new NotImplementedException();
    }

    public Task seedPokemons()
    {
        throw new NotImplementedException();
    }
}