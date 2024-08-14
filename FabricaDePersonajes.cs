using System;

namespace Juego
{
    public class Personaje
    {
        private Datos InfoDatos;
        private Caracteristicas InfoCaracteristicas;

        public Personaje(Datos InfoD, Caracteristicas InfoC)
        {
            InfoDatos = InfoD;
            InfoCaracteristicas = InfoC;
        }

        public int calcularAtaque(TipoPokemon Elemento, int Dano)
        {
            
        }

        public void recibirAtaque(TipoPokemon Elemento, int Dano)
        {
            
        }

        public void mostrarEstadisticas()
        {
            WriteLine("\n");
            mostrarTextoCentrado("===================================", ConsoleColor.White);

            string stats = string.Format("{0,15} {1,20}", "Nombre", InfoDatos.Nombre);
            mostrarTextoCentrado(stats, ConsoleColor.White);

            stats = string.Format("{0,15} {1,20}", "Tipo", InfoDatos.Tipo1);
            mostrarTextoCentrado(stats, ConsoleColor.White);
            if (!IsNullOrEmpty(InfoDatos.Tipo2))
            {
                stats = string.Format("{-15} {0,20}", InfoDatos.Tipo2);
                mostrarTextoCentrado(stats, ConsoleColor.White);
            }
            
            stats = string.Format("{0,15} {1,20}", "Debilidades", InfoDatos.Debilidades[0]);
            mostrarTextoCentrado(stats, ConsoleColor.White);
            for (int i = 1; i < InfoDatos.Debilidades.Count; i++)
            {
                stats = string.Format("{-15} {0,20}", Debilidades[i]);
                mostrarTextoCentrado(stats, ConsoleColor.White);
            }

            stats = string.Format("{0,15} {1,20}", "Salud", InfoCaracteristicas.Salud);
            mostrarTextoCentrado(stats, ConsoleColor.White);

            stats = string.Format("{0,15} {1,20}", "Fuerza", InfoCaracteristicas.Fuerza);
            mostrarTextoCentrado(stats, ConsoleColor.White);

            stats = string.Format("{0,15} {1,20}", "Nivel", InfoCaracteristicas.Nivel);
            mostrarTextoCentrado(stats, ConsoleColor.White);


            stats = string.Format("{0,15} {1,20}", "Velocidad", InfoCaracteristicas.Velocidad);
            mostrarTextoCentrado(stats, ConsoleColor.White);
            mostrarTextoCentrado("===================================", ConsoleColor.White);
        }
    }
}