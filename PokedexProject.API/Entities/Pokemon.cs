using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokedexProject.API.Entities;

//[Table("Pokemon")]
public class Pokemon
{
    public int id { get; set; }
    [Required]
    public string name { get; set; }
    public string url { get; set; }
}