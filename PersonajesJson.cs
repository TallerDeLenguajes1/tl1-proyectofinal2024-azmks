using System.IO;
using System.Text.Json;
using static System.Console;

namespace Juego
{
    public static class PersonajesJson
    {
        public static void GuardarPersonajes(List<Personaje> listaPersonajes, string RutaArchivo = "personajes.json")
        {
            // Intento serializar la lista de personajes recibida y escribirla en el archivo especificado. Muestra los errores en caso
            // de un intento fallido
            try
            {
                string misPersonajes = JsonSerializer.Serialize(listaPersonajes);
                File.WriteAllText(RutaArchivo, misPersonajes);
            }
            catch (JsonException error)
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"Error al procesar el archivo Json '{RutaArchivo}': {error.Message}.");
                ResetColor();
            }
            catch (IOException error)
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"Error al abrir/escribir el archivo '{RutaArchivo}': {error.Message}.");
                ResetColor();
            }
            catch (Exception error)
            {
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"Error al guardar el archivo '{RutaArchivo}': {error.Message}.");
                ResetColor();
            }
        }

         public static List<Personaje> LeerPersonajes(string RutaArchivo = "personajes.json")
        {
            // Intento acceder al archvio en RutaArchivo y deserializarlo. Si se logro, retorno la lista de personajes obtenido.
            // De lo contrario, retorno una lista vacia.
            if (File.Exists(RutaArchivo))
            {
                try
                {
                    string Personajes = File.ReadAllText(RutaArchivo);
                    List<Personaje> misPersonajes = JsonSerializer.Deserialize<List<Personaje>>(Personajes);

                    return misPersonajes;
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al abrir/leer el archivo '{RutaArchivo}': {error.Message}.");
                    ResetColor();

                    return new List<Personaje>();
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al procesar el archivo Json '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return new List<Personaje>();
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al leer el archivo '{RutaArchivo}': {error.Message}.");
                    ResetColor();
                    
                    return new List<Personaje>();
                }
            }
            else 
            {    
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine($"Error. El archivo '{RutaArchivo}' no se encontro o esta vacio.");
                ResetColor();
                    
                return new List<Personaje>();
            }
        }

    }
}