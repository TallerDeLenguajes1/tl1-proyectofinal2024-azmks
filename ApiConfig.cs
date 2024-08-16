using System.Text.Json;
using static System.Console;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Juego
{
    public class ApiConfig
    {
        private static readonly HttpClient cliente = new HttpClient();
        private Random Semilla = new Random();

        public async Task<PokemonJson> PokemonRandom()
        {
            int id = Semilla.Next(1, 151);
            try
            {
                PokemonJson poke = await Pokemon(id);
                return poke;
            }
            catch (HttpRequestException error)
            {
                Interfaz.mostrarTextoCentrado($"No se pudo conectar con la pokeApi. Error: {error.Message}", ConsoleColor.White);
                return null;
            }
            catch (JsonException error)
            {
                Interfaz.mostrarTextoCentrado($"No se pudo procesar la respuesta de la pokeApi. Error: {error.message}", ConsoleColor.White);
                return null;
            }
            catch (Exception error)
            {
                Interfaz.mostrarTextoCentrado($"Error: {error.message}", ConsoleColor.White);
                return null;
            }
        }

        public async Task<PokemonJson> Pokemon(int id)
        {
            try
            {
                string respuesta = await cliente.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
                return JsonSerializer.Deserialize<PokemonJson>(respuesta);
            }
            catch (HttpRequestException error)
            {
                Interfaz.mostrarTextoCentrado($"No se pudo conectar con la pokeApi. Error: {error.Message}", ConsoleColor.White);
                return null;
            }
            catch (JsonException error)
            {
                Interfaz.mostrarTextoCentrado($"No se pudo procesar la respuesta de la pokeApi. Error: {error.message}", ConsoleColor.White);
                return null;
            }
            catch (Exception error)
            {
                Interfaz.mostrarTextoCentrado($"Error: {error.message}", ConsoleColor.White);
                return null;
            }
        }
    }
}