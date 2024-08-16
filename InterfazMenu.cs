using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
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
            Interfaz.mostrarTextoCentrado(Texto, Color);
            
            for (int i = 0; i < Opciones.Count; i++)
            {
                if (i == IndiceSeleccionado) Interfaz.mostrarTextoCentrado(Opciones[i], Color);
                else Interfaz.mostrarTextoCentrado(Opciones[i], ConsoleColor.White);
            }

            WriteLine("\n");
            Interfaz.mostrarTextoCentrado("Cambie de opcion utilizando las teclas de flecha hacia arriba o hacia abajo y luege presione Enter para continuar.", ConsoleColor.DarkGray);
        }

        public int Ejecutar(ConsoleColor Color) {
            Clear();
            Mostrar();
            ConsoleKey TeclaPresionada;
            int Indice = IndiceSeleccionado;

            do
            {
                TeclaPresionada = ReadKey(true).Key;

                if (TeclaPresionada == ConsoleKey.DownArrow) Indice = (Indice < Opciones.Count - 1) ? Indice + 1 : 0;
                if (TeclaPresionada == ConsoleKey.UpArrow) Indice = (Indice > 0) ? Indice - 1 : Opciones.Count - 1;
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
            List<string> lineas = Texto.Split('\n').ToList();

            ForegroundColor = Color;
            foreach (string contenido in lineas)
            {
                relleno = (anchoDeConsola - contenido.Length) / 2;
                SetCursorPosition(relleno, Console.CursorTop);
                WriteLine(contenido);
            }
            ResetColor();
        }

        public static void mostrarInstrucciones()
        {
            Clear();
            string info = @"
                Este juego esta basado en la famosa franquicia de videojuegos 'Pokemon'. 
                Para el funcionamiento de este juego se utiliza como recurso la api pokeApi
                que contiene una base de datos con la informacion de los distintos pokemones,
                items, juegos y generaciones producto de esta saga.  Sin embargo, este juego
                solo hara uso de los pokemones pertenecientes a la primera generacion.
                ";

            string instrucciones = @"
                El juego consiste en 10 rondas donde deberas enfrentarte a difernetes jefes
                para pasar a la siguiente ronda.
                Al principio podras elegir 3 pokemon aleatorios para utilizar durante el combate
                contra los jefes.
                Al final de cada ronda podras elegir entre subir de nivel de nivel a un pokemon
                o curarlo.
                El juego finalizara en el caso que pierdas contra uno de los jefes o cuando
                hayas derrotado al ultimo de ellos.
            ";

            Interfaz.mostrarTextoCentrado(info, ConsoleColor.DarkGray);
            Interfaz.mostrarTextoCentrado(instrucciones, ConsoleColor.White);
            Interfaz.mostrarTextoCentrado("Presiona cualquier tecla para continuar...", ConsoleColor.White);
            ReadKey(true);
        }

        public static string obtenerNombre()
        {
            Clear();
            string instruccion = "¿Como te llamas?\nTu nombre debe tener una longitud entre 3 y 10 caracteres (letras o digitos). Ademas, debe comenzar con una letra.\n";
            string nombre = String.Empty;

            do
            {
                Clear();
                Interfaz.mostrarTextoCentrado(instruccion, ConsoleColor.Cyan);
                SetCursorPosition((WindowWidth-10)/2, Console.CursorTop);
                nombre = ReadLine();
            }
            while (nombre.Length < 3 || nombre.Length > 10 || !Extra.esNombreValido(nombre));

            Clear();
            ResetColor();
            return nombre;
        }
        
        public static void mostrarMensajePrebatalla()
        {
            string mensaje = @"
            
   _        __            _                 _ 
  /_\      / / _   _  ___| |__   __ _ _ __ / \
 //_\\    / / | | | |/ __| '_ \ / _` | '__/  /
/  _  \  / /__| |_| | (__| | | | (_| | | /\_/ 
\_/ \_/  \____/\__,_|\___|_| |_|\__,_|_| \/   
                                              
            ";
            Interfaz.mostrarTextoCentrado(mensaje, ConsoleColor.Cyan);
        }

        public static void mostrarMensajeGanaste()
        {
            string mensaje = @"
   ___                      _         _ 
  / _ \__ _ _ __   __ _ ___| |_ ___  / \
 / /_\/ _` | '_ \ / _` / __| __/ _ \/  /
/ /_\\ (_| | | | | (_| \__ \ ||  __/\_/ 
\____/\__,_|_| |_|\__,_|___/\__\___\/   
                                        
            ";
            Interfaz.mostrarTextoCentrado("\nVenciste a todos los jefes!\n", ConsoleColor.Green);
            Interfaz.mostrarTextoCentrado(mensaje, ConsoleColor.Green);
        }
        
        public static void mostrarMensajePerdiste()
        {
            string mensaje = @"
   ___             _ _     _         _ 
  / _ \___ _ __ __| (_)___| |_ ___  / \
 / /_)/ _ \ '__/ _` | / __| __/ _ \/  /
/ ___/  __/ | | (_| | \__ \ ||  __/\_/ 
\/    \___|_|  \__,_|_|___/\__\___\/    
            ";
            Interfaz.mostrarTextoCentrado("\nTe quedaste sin pokemones!\n", ConsoleColor.Red);
            Interfaz.mostrarTextoCentrado(mensaje, ConsoleColor.Red);
        }
    }
}