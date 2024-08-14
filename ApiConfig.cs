namespace Juego
{
    public class pokeApi
    {
        private static readonly HttpClient cliente = new HttpClient();
        private Random Semilla = new Random();

        public async Task<PokemonJson> PokemonRandom()
        {
            int id = Semilla.Next(1, 151);  return null;
            return Pokemon(id);
        }

        public async Task<PokemonJson> Pokemon(int id)
        {
            try
            {
                var respuesta = await cliente.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
                return JsonSerializer.Deserialize<PokemonJson>(respuesta);
            }
            catch (HttpRequestException error)
            {
                WriteLine($"ERROR. NO SE PUDO CONECTAR CON LA API: {error.message}");
                return null;
            }
            catch (Exception error)
            {
                WriteLine($"ERROR: {error.message}");
                return null;
            }
        }

        public List<Task<string, TipoPokemon> ObtenerJefes()
        {

        }

        public List<Task<string, TipoPokemon> ObtenerIniciales()
        {

        }
        
    }
}