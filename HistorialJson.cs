using System.IO;
using System.Text.Json;
using System.Linq;
using static System.Console;
using System.Text.Json.Serialization;

namespace Juego
{
    public class Ganador
    {
        [JsonPropertyName("Nombre")]
        public string Nombre {get; set;}
        [JsonPropertyName("Ganadores")]
        public List<string> HeroesGanadores {get; set;}

        [JsonConstructor]
        public Ganador(string Nombre, List<string> HeroesGanadores)
        {
            this.Nombre = Nombre;
            this.HeroesGanadores = HeroesGanadores;
        }

        public Ganador(string Nombre, List<Personaje> listaGanadores)
        {
            this.Nombre = Nombre;
            HeroesGanadores = new List<string>();
            foreach (Personaje Heroe in listaGanadores)
            {
                if (Heroe.InfoCaracteristicas.Salud > 0) HeroesGanadores.Add(Heroe.InfoDatos.Nombre);
            }
        }
    }

    public static class HistorialJson
    {

        // Guarda una partida en la carpeta 'Partidas'
        public static void GuardarPartida(Partida MiParti)
        {
            string RutaArchivo = $"Partidas\\{MiParti.Nombre}.json";
            try
            {
                // Serializo la lista para guardarla en el archivo con el nuevo ganador añadido
                string partidajson = JsonSerializer.Serialize(MiParti);
                File.WriteAllText(RutaArchivo, partidajson);
            }
            catch (IOException error)
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"Error al guardar partida. No se pudo abrir/escribir el archivo '{RutaArchivo}': {error.Message}.");
                ResetColor();
            }
            catch (JsonException error)
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"Error al guardar partida. No se pudo procesar el archivo Json '{RutaArchivo}': {error.Message}.");
                ResetColor();
            }
            catch (Exception error)
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"Error al guardar partida: '{RutaArchivo}': {error.Message}.");
                ResetColor();
            }
        }

        // Dado la ruta del archivo json de una partida, devuelve la partida contenida en el archivo
        public static Partida CargarPartida(string RutaPartida)
        {
            string RutaArchivo = $"{RutaPartida}";
            if (Extra.Existe(RutaArchivo) && !Extra.EstaVacio(RutaArchivo))
            {
                try
                {
                    // Serializo la lista para guardarla en el archivo con el nuevo ganador añadido
                    string partidajson = File.ReadAllText(RutaArchivo);
                    Partida MiParti = JsonSerializer.Deserialize<Partida>(partidajson);

                    WriteLine(partidajson);
                    return MiParti;
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar partida. No se pudo abrir/escribir el archivo en '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return null;
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar partida. No se pudo procesar el archivo Json en '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return null;
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar partida en '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return null;
                }
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"No existe el archivo en '{RutaArchivo}'.");
                ResetColor();
                
                return null;
            }
        }

        // Intenta obtener una lista de archivos de partidas en la carpeta 'Partidas' y en caso de no poder devuelve una lista vacia
        public static List<string> ObtenerListaPartidas()
        {
            string RutaCarpeta = @"Partidas\";
            List<string> listaPartidas = [];
            try
            {
                listaPartidas = Directory.EnumerateFileSystemEntries(RutaCarpeta).ToList();
                return listaPartidas;
            }
            catch (IOException error)
            {
                WriteLine($"Error al obtener partidas de {RutaCarpeta}: {error.Message}");
                return listaPartidas;
            }
            catch (Exception error)
            {
                WriteLine($"Error al obtener partidas de {RutaCarpeta}: {error.Message}");
                return listaPartidas;
            }
        }

        // Añade un ganador al archivo 'ganadores.json'
        public static void GuardarGanador(Ganador nuevoGanador, string RutaArchivo = "ganadores.json")
        {
            if (Extra.Existe(RutaArchivo) && !Extra.EstaVacio(RutaArchivo))
            {
                try
                {
                    // Leo el archivo para obtener la lista de ganadores y agregarle el nuevo ganador
                    List<Ganador> listaGanadores = LeerGanadores();
                    listaGanadores.Add(nuevoGanador);

                    // Elimino la partida guardada el ganador
                    File.Delete($"Partidas\\{nuevoGanador.Nombre}.json");

                    // Serializo la lista para guardarla en el archivo con el nuevo ganador añadido
                    string ganadoresJson = JsonSerializer.Serialize(listaGanadores);
                    File.WriteAllText(RutaArchivo, ganadoresJson);
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar. No se pudo abrir/escribir el archivo '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar. No se pudo procesar el archivo Json '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar: '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                }
            }
            else
            {
                try
                {
                    // Creo una nueva lista de ganadores y le añado el nuevo ganador
                    List<Ganador> listaGanadores = [nuevoGanador];

                    // Elimino la partida guardada el ganador
                    File.Delete($"Partidas\\{nuevoGanador.Nombre}.json");

                    // Serializo la lista de ganadores y la escribo en un nuevo archivo json
                    string ganadoresJson = JsonSerializer.Serialize(listaGanadores);
                    File.WriteAllText(RutaArchivo, ganadoresJson);
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar. No se pudo abrir/escribir el archivo '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar. No se pudo procesar el archivo Json '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al guardar: '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                }
            }
        }
        public static List<Ganador> LeerGanadores()
        {
            string RutaArchivo = "ganadores.json";
            // Inicializo una nnueva lista de ganadores y si existe el archivo, no esta vacio y no hubo errores al abrir, leer
            // o procesar el archivo json devuelve la lista obtenida. Caso contrario devuelve una lista vacia
            List<Ganador> listaGanadores = new List<Ganador>();
            if (Extra.Existe(RutaArchivo) && !Extra.EstaVacio(RutaArchivo))
            {
                try
                {
                    string ganadoresJson = File.ReadAllText(RutaArchivo);
                    listaGanadores = JsonSerializer.Deserialize<List<Ganador>>(ganadoresJson);

                    return listaGanadores;
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al leer. No se pudo abrir/leer el archivo '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return listaGanadores;
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al leer. No se pudo procesar el archivo Json '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return listaGanadores;
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al leer el archivo '{RutaArchivo}': {error.Message}.");
                    ResetColor();

                    return listaGanadores;
                }
            }
            else
            {
                Interfaz.MostrarTextoCentrado("Todavia no hay Ganadores", ConsoleColor.DarkGray);
                return listaGanadores;
            }
        }
    }
}