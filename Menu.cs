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

        public void Mostrar(ConsoleColor Color, bool MostrarInvertido = false)
        {
            Interfaz.MostrarTextoCentrado(Texto, Color);
            
            if (MostrarInvertido)
            {
                for (int i = Opciones.Count - 1; i >= 0; i--)
                {
                    if (i == IndiceSeleccionado) Interfaz.MostrarTextoCentrado($"[{Opciones[i]}]", Color);
                    else Interfaz.MostrarTextoCentrado($"{Opciones[i]}");
                }
            }
            else
            {            
                for (int i = 0; i < Opciones.Count; i++)
                {
                    if (i == IndiceSeleccionado) Interfaz.MostrarTextoCentrado($"[{Opciones[i]}]", Color);
                    else Interfaz.MostrarTextoCentrado($"{Opciones[i]}");
                }
            }

            WriteLine("\n");
            Interfaz.MostrarTextoCentrado("Cambie de opcion utilizando las teclas de flecha hacia arriba o hacia abajo.", ConsoleColor.DarkGray);
            Interfaz.MostrarTextoCentrado("Luego presione [Enter] para continuar.", ConsoleColor.DarkGray);
        }

        public int Ejecutar(ConsoleColor Color)
        {
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

        public int EjecutarBatalla(Partida MiPartida)
        {
            Clear();
            ConsoleKey TeclaPresionada;
            IndiceSeleccionado = Opciones.Count-1;
            int Indice = IndiceSeleccionado;

            do
            {
                Clear();
                MiPartida.MostrarBatalla();
                Mostrar(ConsoleColor.Yellow, true);
                TeclaPresionada = ReadKey(true).Key;

                if (TeclaPresionada == ConsoleKey.DownArrow) Indice = (Indice > 0) ? Indice - 1 : Opciones.Count - 1;
                if (TeclaPresionada == ConsoleKey.UpArrow) Indice = (Indice < Opciones.Count - 1) ? Indice + 1 : 0;
                IndiceSeleccionado = Indice;
            }
            while (TeclaPresionada != ConsoleKey.Enter);

            return IndiceSeleccionado;
        }
    }
}