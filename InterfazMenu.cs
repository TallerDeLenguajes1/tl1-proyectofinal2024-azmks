using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Juego
{
    public class Menu
    {
        private string Texto;
        private string[] Opciones;
        private int IndiceSeleccionado;

        public Menu(string T, string[] O)
        {
            Texto = T;
            Opciones = O;
            IndiceSeleccionado = 0;
        }

        public void Mostrar()
        {
            Interfaz.mostrarTextoCentrado(Texto, ConsoleColor.Cyan);
            
            for (int i = 0; i < Opciones.Length; i++)
            {
                if (i == IndiceSeleccionado) Interfaz.mostrarTextoCentrado(Opciones[i], ConsoleColor.Cyan);
                else Interfaz.mostrarTextoCentrado(Opciones[i], ConsoleColor.White);
            }

            WriteLine("\n");
            Interfaz.mostrarTextoCentrado("Cambie de opcion utilizando las teclas de flecha hacia arriba o hacia abajo y luege presione Enter para continuar.", ConsoleColor.DarkGray);
        }

        public int Ejecutar() {
            Clear();
            Mostrar();
            ConsoleKey TeclaPresionada;
            int Indice = IndiceSeleccionado;

            do
            {
                TeclaPresionada = ReadKey(true).Key;

                if (TeclaPresionada == ConsoleKey.DownArrow) Indice = (Indice < Opciones.Length - 1) ? Indice + 1 : 0;
                if (TeclaPresionada == ConsoleKey.UpArrow) Indice = (Indice > 0) ? Indice - 1 : Opciones.Length - 1;
                IndiceSeleccionado = Indice;

                Clear();
                Mostrar();
            }
            while (TeclaPresionada != ConsoleKey.Enter);

            return IndiceSeleccionado;
        }
    }

    public static class Interfaz
    {   
        public static void mostrarTextoCentrado(string Texto, ConsoleColor Color)
        {
            int anchoDeConsola = WindowWidth;
            int relleno;
            string[] lineas = Texto.Split('\n');

            ForegroundColor = Color;
            foreach (string contenido in lineas)
            {
                relleno = (anchoDeConsola - contenido.Length) / 2;
                SetCursorPosition(relleno, Console.CursorTop);
                WriteLine(contenido);
            }
            ResetColor();
        }

        public static bool esEntradaValida(string palabra)
        {
            bool resultado = true;
            
            if (String.IsNullOrEmpty(palabra)) resultado = false;
            if (Char.IsDigit(palabra[0])) resultado = false;
            
            for (int i = 0; i < palabra.Length; i++) if (!Char.IsLetterOrDigit(palabra,i)) resultado = false;

            return resultado;
        }
    }
}