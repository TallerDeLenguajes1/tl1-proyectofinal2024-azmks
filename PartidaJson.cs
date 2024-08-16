using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static System.Console;

namespace Juego
{
    public class PartidaJson
    {
        [JsonPropertyName("Nombre")]
        public string Nombre {get; set;}
        [JsonPropertyName("Ronda")]
        public int RondaActual {get; set;}
        [JsonPropertyName("Campeones")]
        public List<Personaje> Campeones {get; set;}
        [JsonPropertyName("Jefes")]
        public List<Personaje> Jefes {get; set;}

        public PartidaJson() {}

        public void nuevaPartida(string nombre, List<Personaje> champeons, List<Personaje> bosses)
        {
            RondaActual = 1;
            Nombre = nombre;
            Campeones = champeons;
            Jefes = bosses;
        }

        public void GuardarPartida(int ronda, List<Personaje> champeons, List<Personaje> bosses)
        {
            RondaActual = ronda;
            Campeones = champeons;
            Jefes = bosses;
            string rutaArchivo = $"./Partidas/{Nombre}.json";

            try
            {
                string textoJson = JsonSerializer.Serialize(this);
                File.WriteAllText(textoJson);
            }
            catch (ArgumentNullException error)
            {
                Interfaz.mostrarTextoCentrado($"No se pudo guardar la partida actual. Error: {error.Message}", ConsoleColor.White);
            }
            catch (IOException error)
            {
                Interfaz.mostrarTextoCentrado($"No se pudo guardar la partida actual. Error: {error.Message}", ConsoleColor.White);
            }
            catch (Exception error)
            {
                Interfaz.mostrarTextoCentrado($"No se pudo guardar la partida actual. Error: {error.Message}", ConsoleColor.White);
            }
        }
    }
}