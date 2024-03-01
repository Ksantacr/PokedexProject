using Microsoft.AspNetCore.Mvc;
using PokedexProject.API.Services;

namespace PokedexProject.API.Controllers;

[Route("[controller]")]
public class PokedexController : Controller
{
    private readonly IPokedexService _pokedexService;

    public PokedexController(IPokedexService pokedexService)
    {
        _pokedexService = pokedexService;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Returns pokemons

        var list  = await this._pokedexService.getPokemons();
        return Ok(list);
    }
    
    
    // Post
    [HttpPost]
    public IActionResult Post()
    {
        return Ok("Hello");
    }
}