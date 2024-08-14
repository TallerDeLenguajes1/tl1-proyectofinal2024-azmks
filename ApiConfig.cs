using System.Text.Json;
using static System.Console;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Juego
{
    public class pokeApi
    {
        private static readonly HttpClient cliente = new HttpClient();
        private Random Semilla = new Random();

        public async Task<PersonajesJson> PokemonRandom()
        {
            int id = Semilla.Next(1, 151);
            return Pokemon(id);
        }

        public async Task<PersonajesJson> Pokemon(int id)
        {
            try
            {
                var respuesta = await cliente.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
                return JsonSerializer.Deserialize<PersonajesJson>(respuesta);
            }
            catch (HttpRequestException error)
            {
                WriteLine($"ERROR. NO SE PUDO CONECTAR CON LA API: {error.Message}");
                return null;
            }
            catch (Exception error)
            {
                WriteLine($"ERROR: {error.message}");
                return null;
            }
        }
        public List<Task<string, TipoPokemon> ObtenerIniciales()
        {
            int cant = 0;
            List<

            do
            {
                int temp_id = Semilla.Next(1, 152);
                Pokemon poke_temp = new Personaje();

                }
            }
            while (cant < 3);
        }
        
    }
}