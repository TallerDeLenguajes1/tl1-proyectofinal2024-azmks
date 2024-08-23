using static System.Console;
using System.Text.Json.Serialization;

namespace Juego
{
    public class PokemonJson
    {
        [JsonPropertyName("id")]
        public int id {get; set;}
        [JsonPropertyName("name")]
        public string name {get; set;}
        [JsonPropertyName("types")]
        public List<typeInfo> types {get; set;}

        public void Mostrar()
        {
            ForegroundColor = ConsoleColor.Green;

            string idLinea = string.Format("Id: {0,26}", id);
            string pokemonLinea = string.Format("Pokemon: {0,21}", name);
            string elementosLinea = "Elementos: ";

            if (types.Count == 1)
            {
                elementosLinea += string.Format("{0,19}", types[0].type.name);
            }
            else if (types.Count == 2)
            {
                string elementos = $" {types[0].type.name}, {types[1].type.name}";
                elementosLinea += string.Format("{0, 19}", elementos);
            }

            WriteLine(idLinea);
            WriteLine(pokemonLinea);
            WriteLine(elementosLinea);
            WriteLine();
            
            ResetColor();
        }
    }
        
    public class typeInfo
    {   
        [JsonPropertyName("type")]
        public typeName type {get; set;}
    }

    public class typeName
    {
        [JsonPropertyName("name")]
        public string name {get; set;}
    }
}