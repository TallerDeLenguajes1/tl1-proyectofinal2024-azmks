using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Juego
{
    public class PersonajesJson
    {
        [JsonPropertyName("id")]
        private int id {get;}
        
        [JsonPropertyName("status")]
        public string estado {get;set;}
        [JsonPropertyName("name")]
        public string nombre {get;}
        [JsonPropertyName("url")]
        public string url {get;}

        public PersonajesJson(int num, string palabra, string link, string est)
        {
            id = num;
            nombre = palabra;
            url = link;
            estado = est;
        }

        public void GuardarPersonajes(List<Pokemon> pokeLista, string nombreDeArchivo)
        {
            try
            {
                var opciones = new JsonSerializerOptions;
                using (var archivo = new FileStream(nombreDeArchivo, FileMode.Create))
                {
                    using (var strWriter = new StreamWriter(archivo))
                    {
                        string archivo = JsonSerializer.Serialize(personajes, opciones);
                        strWriter.WriteLine(archivo);
                        strWriter.Flush();
                    }
                }
            }
            catch (Exception error)
            {
                Console.ForeGroundColor = ConsoleColor.Red;
                Console.WriteLine($"No se pudo guardar el archivo: '{nombreArchivo}'. ERROR: {error.Message}");
                Console.ResetColor();
            }
        }

        public void LeerPersonajes() {

        }
    }
}