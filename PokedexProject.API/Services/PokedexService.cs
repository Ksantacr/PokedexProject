using Newtonsoft.Json;
using PokedexProject.API.Models;

namespace PokedexProject.API.Services;

public interface IPokedexService
{
    Task<IList<Pokemon>> getPokemons();
    Task<Pokemon> getPokemon(int id);
    Task<Pokemon> createPokemon(Pokemon pokemon);
    Task<Pokemon> updatePokemon(int id, Pokemon pokemonUpdated);
    Task deletePokemon(int id);
    Task seedPokemons();
}

public class PokedexService : IPokedexService
{
    private readonly HttpClient _http;
    private IList<Pokemon> _pokemons;

    public PokedexService(HttpClient http)
    {
        _http = http;
        _pokemons = new List<Pokemon>();
    }
    public async Task<IList<Pokemon>> getPokemons()
    {
        var result  = await _http.GetAsync("https://pokeapi.co/api/v2/pokemon");
        //var result  = await _http.GetFromJsonAsync<object>("https://pokeapi.co/api/v2/pokemon");

        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<PokemonResultAPI>(json);
            return jsonResult.results;
        }
        
        return new List<Pokemon>();
    }

    public Task<Pokemon> getPokemon(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Pokemon> createPokemon(Pokemon pokemon)
    {
        throw new NotImplementedException();
    }

    public Task<Pokemon> updatePokemon(int id, Pokemon pokemonUpdated)
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