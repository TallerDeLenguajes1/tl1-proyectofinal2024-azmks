using System.Text.Json;
using static System.Console;

namespace Juego
{    
    public class PokeApi
    {
        private static readonly HttpClient Cliente = new HttpClient();

        public static async Task<PokemonJson> Pokemon(int Id)
        {
            try
            {
                string Respuesta = await Cliente.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{Id}");
                var miPoke = JsonSerializer.Deserialize<PokemonJson>(Respuesta);

                return miPoke;
            }
            catch (HttpRequestException error)
            {
                ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al intentar conectar con la pokeApi: {error.Message}.");
                if (error.Message == "Host Desconocido. pokeapi:443")
                    Console.WriteLine($"Por favor asegurese de estar conectado a internet.");
                Console.ResetColor();

                return null;
            }
            catch (JsonException error)
            {
                ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al intentar procesar la respuesta de la pokeApi: {error.Message}.");
                Console.ResetColor();

                return null;
            }
            catch (Exception error)
            {
                ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al intentar obtener un pokemon de la pokeApi: {error.Message}.");
                Console.ResetColor();
                
                return null;
            }
        }

    }
}