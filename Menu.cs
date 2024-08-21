using System;
using System.Text;
using System.Linq;
using static System.Console;

namespace Juego
{
    public class Menu
    {
        private string Texto;
        private List<string> Opciones;
        private int IndiceSeleccionado;

        public Menu(string T, List<string> O)
        {
            Texto = T;
            Opciones = new List<string>();
            foreach(string opcion in O)
            {
                Opciones.Add(opcion);
            }
            IndiceSeleccionado = 0;
        }

        public void Mostrar(ConsoleColor Color)
        {
            Interfaz.MostrarTextoCentrado(Texto, Color);
            
            for (int i = 0; i < Opciones.Count; i++)
            {
                if (i == IndiceSeleccionado) Interfaz.MostrarTextoCentrado(Opciones[i], Color);
                else Interfaz.MostrarTextoCentrado(Opciones[i], ConsoleColor.White);
            }

            WriteLine("\n");
            Interfaz.MostrarTextoCentrado("Cambie de opcion utilizando las teclas de flecha hacia arriba o hacia abajo y luege presione Enter para continuar.", ConsoleColor.DarkGray);
        }

        public int Ejecutar(ConsoleColor Color) {
            Clear();
            Mostrar(Color);
            ConsoleKey TeclaPresionada;
            int Indice = IndiceSeleccionado;

            do
            {
                TeclaPresionada = ReadKey(true).Key;

                if (TeclaPresionada == ConsoleKey.DownArrow) Indice = (Indice < Opciones.Count - 1) ? Indice + 1 : 0;
                if (TeclaPresionada == ConsoleKey.UpArrow) Indice = (Indice > 0) ? Indice - 1 : Opciones.Count - 1;
                IndiceSeleccionado = Indice;

                Clear();
                Mostrar(Color);
            }
            while (TeclaPresionada != ConsoleKey.Enter);

            return IndiceSeleccionado;
        }
    }
}