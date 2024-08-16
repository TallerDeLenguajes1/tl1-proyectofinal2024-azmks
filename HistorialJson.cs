using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Juego
{
    public class Ganador
    {
        [JsonPropertyName("Nombre")]
        public string Nombre {get; set;}
        [JsonPropertyName("CampeonesGanadores")]
        public List<string> CampeonesGanadores {get; set;}
        public Ganador(string name, List<string> lista)
        {
            Nombre = name;
            CampeonesGanadores = new List<string>();
            foreach (string perso in lista)
            {
                CampeonesGanadores.Add(perso);
            }
        }
    }

    public class HistorialJson
    {
        public void GuardarGanador(Ganador nuevoGanador, string rutaArchivo)
        {
            if (Extra.Existe(rutaArchivo))
            {
                try
                {
                    // Leo el archivo para obtener la lista de ganadores y agregarle el nuevo ganador
                    string listaGanadoresJson = File.ReadAllText(rutaArchivo);
                    List<Ganador> listaGanadores = LeerGanadores(rutaArchivo);
                    if (listaGanadores != null)
                    {
                        listaGanadores.Add(nuevoGanador);
                    }
                    else
                    {
                        listaGanadores = new List<Ganador>();
                        listaGanadores.Add(nuevoGanador);
                    }

                    // Serializo la lista para guardarla en el archivo con el nuevo ganador añadido
                    string ganadoresJson = JsonSerializer.Serialize(listaGanadores);
                    File.WriteAllText(rutaArchivo, ganadoresJson);
                }
                catch (ArgumentNullException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar el ganador. Error: {error.Message}", ConsoleColor.White);
                }
                catch (IOException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar el ganador. Error: {error.Message}", ConsoleColor.White);
                }
                catch (JsonException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar el ganador. Error: {error.Message}", ConsoleColor.White);
                }
                catch (Exception error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar el ganador. Error Inesperado: {error.Message}", ConsoleColor.White);
                }
            }
            else
            {
                
                try
                {
                    // Creo una nueva lista de ganadores y le añado el nuevo ganador
                    List<Ganador> listaGanadores = new List<Ganador>();
                    ganadoresJson.Add(nuevoGanador);

                    // Serializo la lista de ganadores y la escribo en un nuevo archivo json
                    string ganadoresJson = JsonSerializer.Serialize(listaGanadores);
                    File.WriteAllText(rutaArchivo, ganadoresJson);
                }
                catch (JsonException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar el ganador en el archivo Json. Error: {error.Message}", ConsoleColor.White);
                }
                catch (Exception error)
                {
                    
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar el ganador. Error Inesperado: {error.Message}", ConsoleColor.White);
                }
            }
        }
        public List<Ganador> LeerGanadores(string rutaArchivo)
        {
            if (Extra.Existe(rutaArchivo))
            {
                try
                {
                    string ganadoresJson = File.ReadAllText(rutaArchivo);
                    List<Ganador> listaGanadores = JsonSerialize.Deserialize<List<Ganador>>(ganadoresJson);
                    return listaGanadores;
                }
                catch (JsonException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo leer los ganadores del archivo Json. Error: {error.Message}", ConsoleColor.White);
                    return null;
                }
            }
            else
            {
                Interfaz.mostrarTextoCentrado("Error. No existe el archivo para leer el ganador", ConsoleColor.White);
                return null;
            }
        }
    }
}