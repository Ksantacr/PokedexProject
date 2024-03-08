using System.ComponentModel.DataAnnotations;

namespace PokedexProject.API.Dtos;

public class PokemonDto
{
    [Required]
    [MinLength(1)]
    public string name { get; set; }
}