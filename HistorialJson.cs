using System.IO;
using System.Text.Json;
using static System.Console;
using System.Text.Json.Serialization;

namespace Juego
{
    public class Ganador
    {
        [JsonPropertyName("Nombre")]
        public string Nombre {get; set;}
        [JsonPropertyName("Ganadores")]
        public List<string> CampeonesGanadores {get; set;}

        public Ganador(string Nombre, List<string> listaGanadores)
        {
            this.Nombre = Nombre;
            CampeonesGanadores = new List<string>();
            foreach (string Campeon in listaGanadores)
            {
                CampeonesGanadores.Add(Campeon);
            }
        }
    }

    public static class HistorialJson
    {
        public static void GuardarGanador(Ganador nuevoGanador, string rutaArchivo)
        {
            if (Extra.Existe(rutaArchivo) && !Extra.EstaVacio(rutaArchivo))
            {
                try
                {
                    // Leo el archivo para obtener la lista de ganadores y agregarle el nuevo ganador
                    List<Ganador> listaGanadores = LeerGanadores(rutaArchivo);
                    listaGanadores.Add(nuevoGanador);

                    // Serializo la lista para guardarla en el archivo con el nuevo ganador añadido
                    string ganadoresJson = JsonSerializer.Serialize(listaGanadores);
                    File.WriteAllText(rutaArchivo, ganadoresJson);
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al guardar. No se pudo abrir/escribir el archivo '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al guardar. No se pudo procesar el archivo Json '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al guardar: '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                }
            }
            else
            {
                try
                {
                    // Creo una nueva lista de ganadores y le añado el nuevo ganador
                    List<Ganador> listaGanadores = new List<Ganador>();
                    listaGanadores.Add(nuevoGanador);

                    // Serializo la lista de ganadores y la escribo en un nuevo archivo json
                    string ganadoresJson = JsonSerializer.Serialize(listaGanadores);
                    File.WriteAllText(rutaArchivo, ganadoresJson);
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al guardar. No se pudo abrir/escribir el archivo '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al guardar. No se pudo procesar el archivo Json '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al guardar: '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                }
            }
        }
        public static List<Ganador> LeerGanadores(string rutaArchivo)
        {
            if (Extra.Existe(rutaArchivo) && !Extra.EstaVacio(rutaArchivo))
            {
                try
                {
                    string ganadoresJson = File.ReadAllText(rutaArchivo);
                    List<Ganador> listaGanadores = JsonSerializer.Deserialize<List<Ganador>>(ganadoresJson);

                    return listaGanadores;
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al leer. No se pudo abrir/leer el archivo '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return null;
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al leer. No se pudo procesar el archivo Json '{rutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return null;
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Error al leer el archivo '{rutaArchivo}': {error.Message}.");
                    ResetColor();

                    return null;
                }
            }
            else
            {
                Interfaz.MostrarTextoCentrado("Todavia no hay Ganadores");
                return null;
            }
        }
    }
}