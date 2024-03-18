using Microsoft.AspNetCore.Mvc;
using PokedexProject.API.Dtos;
using PokedexProject.API.Entities;
using PokedexProject.API.Services;

namespace PokedexProject.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PokedexController : ControllerBase
{
    private readonly IPokedexService _pokedexService;

    public PokedexController(IPokedexService pokedexService)
    {
        _pokedexService = pokedexService;
    }
    
    // [HttpGet("db")]
    // public IActionResult TestDb()
    // {
    //     _dbContext.Database.EnsureCreated();
    //     var list  = _dbContext.Pokemons.ToList();
    //     return Ok(list);
    // }
    
    
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Returns pokemons

        var list  = await this._pokedexService.getPokemons();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPokemon(long id)
    {
        try
        {
            var result = await this._pokedexService.getPokemon(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.);
            return BadRequest(ex.Message);
        }
    }
    
    
    // Post
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PokemonDto pokemonDto)
    {
        var result = await this._pokedexService.createPokemon(pokemonDto);
        return Ok(result);
    }
}