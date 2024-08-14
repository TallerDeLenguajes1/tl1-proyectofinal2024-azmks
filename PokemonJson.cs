using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Juego
{
    public class PokemonJson
    {
        [JsonPropertyName("id")]
        private int id {get;}
        
        [JsonPropertyName("status")]
        public string estado {get;set;}
        [JsonPropertyName("name")]
        public string nombre {get;}
        [JsonPropertyName("url")]
        public string url {get;}

        public PokemonJson(int num, string palabra, string link, string est)
        {
            id = num;
            nombre = palabra;
            url = link;
            estado = est;
        }
    }
}