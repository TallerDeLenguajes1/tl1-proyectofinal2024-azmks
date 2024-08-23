using static System.Console;
using System.Text.Json.Serialization;

namespace Juego
{
    public class Personaje
    {
        [JsonPropertyName("Datos")]
        public Datos InfoDatos {get; set;}
        [JsonPropertyName("Caracteristicas")]
        public Caracteristicas InfoCaracteristicas {get; set;}

        public Personaje(Datos InfoDatos, Caracteristicas InfoCaracteristicas)
        {
            this.InfoDatos = InfoDatos;
            this.InfoCaracteristicas = InfoCaracteristicas;
        }

        public void MostrarPersonaje()
        {
            if (this != null)
            {     
                MostrarDatos();
                MostrarCaracteristicas();
            }
        }
        
        public void MostrarDatos()
        {
            if (this.InfoDatos != null)
            {
                ConsoleColor Color = ConsoleColor.Magenta;

                Interfaz.MostrarTextoCentrado($"Nombre: {InfoDatos.Nombre}",Color);
                Interfaz.MostrarTextoCentrado($"Edad: {InfoDatos.Edad}",Color);
                Interfaz.MostrarTextoCentrado($"Tipos: {String.Join(", ",InfoDatos.Tipos)}", Color);
                Interfaz.MostrarTextoCentrado($"Genero: {InfoDatos.Genero}", Color);

                ResetColor();
            }
        }

        public void MostrarCaracteristicas()
        {
            ConsoleColor Color = ConsoleColor.Yellow;

            if (this.InfoCaracteristicas != null)
            {
                Interfaz.MostrarTextoCentrado($"Salud: {InfoCaracteristicas.Salud}",Color);
                Interfaz.MostrarTextoCentrado($"Fuerza: {InfoCaracteristicas.Fuerza}",Color);
                Interfaz.MostrarTextoCentrado($"Defensa: {InfoCaracteristicas.Defensa}",Color);
                Interfaz.MostrarTextoCentrado($"Nivel: {InfoCaracteristicas.Nivel}",Color);
            }

            ResetColor();
        }
    }
}