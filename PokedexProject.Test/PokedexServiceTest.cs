using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PokedexProject.API.Models;
using PokedexProject.API.Services;

namespace PokedexProject.Test;

public class PokedexServiceTest
{
    private readonly PokedexService _pokedexService;

    private readonly PokemonResultAPI pokemonResultApiMock = new()
    {
        count = 1,
        next = "",
        previous = { },
        results = new List<PokemonResult>() { new() { id = 1, name = "kevin", url = "https://pokeapi.co/api/v2/pokemon/1" } }
    };

    public PokedexServiceTest()
    {
        HttpResponseMessage responseMessage = new HttpResponseMessage
        {
            //The StringContent class creates the HTTP body and content headers
            Content = new StringContent(JsonConvert.SerializeObject(pokemonResultApiMock)),
        };
        var moq = new Mock<HttpClientHandler>();

        moq.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()).ReturnsAsync(responseMessage);


        // mock different httpClient calls: https://medium.com/@andrei_diaconu/c-mock-different-httpclient-calls-in-the-same-method-ec5363ebeedd

        moq.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.Is<HttpRequestMessage>(rm =>
                rm.RequestUri.AbsoluteUri.EndsWith(
                    $"https://pokeapi.co/api/v2/pokemon/{pokemonResultApiMock.results.FirstOrDefault().id}")),
            ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage()
        {
            Content = new StringContent(JsonConvert.SerializeObject(pokemonResultApiMock.results.FirstOrDefault()))
        });

        var handler = moq.Object;
        _pokedexService = new PokedexService(new HttpClient(handler), null);
    }

    [Fact]
    public async void Should_return_pokemon_list()
    {
        // Act
        var pokemonList = pokemonResultApiMock.results;

        // Arrange

        var result = await _pokedexService.getPokemons();
        // Assert

        Assert.Equivalent(pokemonList, result, true);
    }

    [Fact]
    public async void Should_get_one_pokemon()
    {
        // Act
        var pokemon = pokemonResultApiMock.results.FirstOrDefault();

        // Arrange

        var result = await _pokedexService.getPokemon(pokemon.id);
        // Assert

        Assert.True(pokemon.id == result.id);
        Assert.True(pokemon.name == result.name);
        Assert.Equal(pokemon.url, result.url);
    }
}