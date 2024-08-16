using System;
using System.IO;
using System.Text;
using System.Text.Json;
using static System.Console;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Juego
{
    public class PersonajesJson
    {
        public void GuardarPersonajes(List<Personaje> pokeLista, string rutaArchivo)
        {
            if (Extra.Existe(rutaArchivo))
            {
                try
                {
                    string listaJson = JsonSerializer.Serialize(pokeLista);
                    File.WriteAllText(rutaArchivo, listaJson);
                }
                catch (ArgumentNullException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar en '{rutaArchivo}'. Error: {error.Message}", ConsoleColor.White);
                }
                catch (IOException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar en '{rutaArchivo}'. Error: {error.Message}", ConsoleColor.White);
                }
                catch (Exception error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo guardar en '{rutaArchivo}'. Error: {error.Message}", ConsoleColor.White);
                }
            }
            else
            {

            }
        }

        public List<Personaje> LeerPersonajes(string rutaArchivo)
        {
            if (Extra.Existe(rutaArchivo))
            {
                try
                {
                    string listaJson = File.ReadAllText(rutaArchivo);
                    List<Personaje> listaDePersonajes = JsonSerializer.Deserialize<List<Personaje>>(listaJson);
                    return listaDePersonajes;
                }
                catch (JsonException error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo procesar el archivo Json con los personajes. Error: {error.Message}", ConsoleColor.White);
                    return null;
                }
                catch (Exception error)
                {
                    Interfaz.mostrarTextoCentrado($"No se pudo leer el archivo Json con los personajes. Error: {error.Message}", ConsoleColor.White);
                    return null;
                }
            }
            else
            {
                Interfaz.mostrarTextoCentrado("Error: No existe el archivo con la lista de personajes", ConsoleColor.White);
                return null;
            }
        }
    }

    public class Extra(string rutaArchivo)
    {
        public bool Existe()
        {
            // Chequea si existe un archivo en la ruta especificada
            if (!File.Exists(rutaArchivo))
            {
                return false;
            }

            // Si existe el archivo entonces verifica que no este vacio, es decir, que su tamaño en bytes no sea 0
            var infoArchivo = new FileInfo(rutaArchivo);
            if (infoArchivo.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool esNombreValido(string palabra)
        {
            bool resultado = true;
            
            // Si la palabra es null o es un string vacio entonces no es un nombre valido
            if (String.IsNullOrEmpty(palabra)) resultado = false;
            // Si la palabra empieza con un digito entonces no es un nombre valido
            if (Char.IsDigit(palabra[0])) resultado = false;
            
            // Verifico que para cada caracter de la palabra, esta sea una letra o digito, caso contrario
            // seteo el resultado en false y salgo del bucle pues no necesito seguir recorriendo la palabra
            for (int i = 0; i < palabra.Length; i++)
            {
                if (!Char.IsLetterOrDigit(palabra,i))
                {
                    resultado = false;
                    break;
                }
            }

            return resultado;
        }
    }

}