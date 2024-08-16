using System;
using System.IO;
using System.Text;
using System.Text.Json;
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

        public void CargarPartida()
        {
            // Obtengo archivos de la carpeta /Partidas
            DirectoryInfo carpeta = new DirectoryInfo("./Partidas"); //Assuming Test is your Folder

            FileInfo[] archivos = d.GetFiles("*.json"); //Getting Text files
            string str = "";

            foreach(FileInfo archivo in archivos )
            {
                str += ", " + file.Name;
            }

            string[] partidas = str.Split(',');

            try
            {
                Menu partidas = new Menu("Que partida desea cargar?", partidas);
                int eleccion partidas.Ejecutar(ConsoleColor.Cyan);
                string partijson = File.ReadAllText(partidas[eleccion]);
                PartidaJson parti = JsonSerializer.Deserialize<PartidaJson>(partijson);
                RondaActual = parti.RondaActual;
                Nombre = parti.Nombre;
                Campeones = parti.Campeones;
                Jefes = parti.Jefes;
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