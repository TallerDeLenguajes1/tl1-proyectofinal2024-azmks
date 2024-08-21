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
                ForegroundColor = ConsoleColor.Magenta;

                WriteLine($"Nombre: {InfoDatos.Nombre}");
                WriteLine($"Edad: {InfoDatos.Edad}");
                if (InfoDatos.Tipos.Count == 1) 
                    WriteLine($"Tipo: {InfoDatos.Tipos[0]}");
                else if (InfoDatos.Tipos.Count == 2) 
                    WriteLine($"Tipos: {InfoDatos.Tipos[0]}, {InfoDatos.Tipos[1]}");
                WriteLine($"Genero: {InfoDatos.Genero}");

                ResetColor();
            }
        }

        public void MostrarCaracteristicas()
        {
            ForegroundColor = ConsoleColor.Yellow;

            if (this.InfoCaracteristicas != null)
            {
                WriteLine($"Salud: {InfoCaracteristicas.Salud}");
                WriteLine($"Fuerza: {InfoCaracteristicas.Fuerza}");
                WriteLine($"Defensa: {InfoCaracteristicas.Defensa}");
                WriteLine($"Nivel: {InfoCaracteristicas.Nivel}");
            }

            ResetColor();
        }
    }
}