namespace Juego
{
    public class PokemonJson
    {
        [JsonPropertyName("id")]
        private int id {get;}
        [JsonPropertyName("name")]
        public string nombre {get;}
        [JsonPropertyName("estado")]
        [JsonPropertyName("url")]
        public string url {get;}

        public PokemonJson(int num, string palabra, string link)
        {
            id = num;
            nombre = palabra;
            url = link;
        }

        public enum estado
    }
}