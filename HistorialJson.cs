using System.Text.Json;
using System.Text.Json.Serialization;

namespace Juego
{
    public class Ganador
    {
        private string Nombre {get;}
        private Personaje Ganador {get;}

        public PersonajeGanador(string N, Personaje P)
        {
            Nombre = N;
            PersonajeGanador = P;
        }
        
        public void guardarGanador(string rutaArchivo)
        {
            string json = JsonSerializer.Serialize();
            try
            {
                File.AppendAllText(rutaArchivo);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error al guardar el ganador: {error.Message}");
            }
        }
        public void leerGanador(PersonajeGanador NuevoGanador, string rutaArchivo)
        {
            if (Extra.Existe(rutaArchivo))
            {
                string infoGanador = File.ReadAllText(pathName);
                Console.WriteLine(infoGanador);
            }
            else
            {
                Console.WriteLine("Error. No existe el archivo para leer el ganador");
            }
        }
    }
}