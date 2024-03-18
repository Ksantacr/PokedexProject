namespace PokedexProject.API.Models;

public class PokemonResult
{
    public long id { get; set; }
    public string name { get; set; }
    public string url { get; set; }
    
}

public class PokemonResultAPI
{
    public int count { get; set; }
    public string next { get; set; }
    public object previous { get; set; }
    public List<PokemonResult> results { get; set; }
}