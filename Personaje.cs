using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Juego
{
    public class Personaje
    {
        public Datos InfoDatos {get; set;}
        public Caracteristicas InfoCaracteristicas {get; set;}

        public Personaje(Datos InfoD, Caracteristicas InfoC)
        {
            this.InfoDatos = InfoD;
            this.InfoCaracteristicas = InfoC;
        }

        public void mostrarEstadisticas()
        {
            WriteLine("\n");
            Interfaz.mostrarTextoCentrado("====================================", ConsoleColor.White);

            string stats = string.Format("{0,-15} {1,-20}", "Nombre", InfoDatos.Nombre);
            Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);

            if (InfoDatos.Tipos.Count == 1)
            {
                stats = string.Format("{0,-15} {1,-20}", "Tipo", InfoDatos.Tipos[0]);
            }
            else if (InfoDatos.Tipos.Count == 2)
            {
                stats = string.Format("{0,-15} {1,-9} {2,-10}", "Tipo", InfoDatos.Tipos[0], InfoDatos.Tipos[1]);
            }
            Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);
            
            stats = string.Format("{0,-15} {1,-20}", "Debilidades", InfoDatos.Debilidades[0]);
            Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);
            for (int i = 1; i < InfoDatos.Debilidades.Count; i++)
            {
                stats = string.Format("{-15} {0,-20}", Debilidades[i]);
                Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);
            }

            stats = string.Format("{0,-15} {1,-20}", "Salud", InfoCaracteristicas.Salud);
            Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);

            stats = string.Format("{0,-15} {1,-20}", "Nivel", InfoCaracteristicas.Nivel);
            Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);

            stats = string.Format("{0,-15} {1,-20}", "Fuerza", InfoCaracteristicas.Fuerza);
            Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);

            stats = string.Format("{0,-15} {1,-20}", "Defensa", InfoCaracteristicas.Defensa);
            Interfaz.mostrarTextoCentrado(stats, ConsoleColor.White);
            Interfaz.mostrarTextoCentrado("====================================", ConsoleColor.White);
        }
    }
    public enum ElementoPokemon
    {
        Acero,
        Agua,
        Bicho,
        Dragon,
        Electrico,
        Fantasma,
        Fuego,
        Hada,
        Hielo,
        Lucha,
        Normal,
        Planta,
        Psiquico,
        Roca,
        Tierra,
        Veneno,
        Volador,
        Ninguno
    }
}