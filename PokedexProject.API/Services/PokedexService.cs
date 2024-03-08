using Newtonsoft.Json;
using PokedexProject.API.Dtos;
using PokedexProject.API.Models;

namespace PokedexProject.API.Services;

public interface IPokedexService
{
    Task<IList<Pokemon>> getPokemons();
    Task<Pokemon> getPokemon(long id);
    Task<Pokemon> createPokemon(PokemonDto pokemonDto);
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
        var result = await _http.GetAsync("https://pokeapi.co/api/v2/pokemon");
        //var result2  = await _http.GetFromJsonAsync<PokemonResultAPI>("https://pokeapi.co/api/v2/pokemon");

        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<PokemonResultAPI>(json);
            return jsonResult.results;
        }

        return new List<Pokemon>();
    }

    public async Task<Pokemon> getPokemon(long id)
    {
        var result = await _http.GetFromJsonAsync<PokemonById>($"https://pokeapi.co/api/v2/pokemon/{id}");

        //throw new Exception("server exception");

        return new Pokemon { id = result.Id, name = result.Name, url = $"https://pokeapi.co/api/v2/pokemon/{id}" };
    }

    public Task<Pokemon> createPokemon(PokemonDto pokemonDto)
    {
        var pokemonId = _pokemons.Count() + 1;
        var pokemon = new Pokemon { name = pokemonDto.name, id = pokemonId, url = $"https://pokeapi.co/api/v2/pokemon/{pokemonId}"};
        _pokemons.Add(pokemon);
        return Task.FromResult<Pokemon>(pokemon);
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