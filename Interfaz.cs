using static System.Console;

namespace Juego
{
    public static class Interfaz
    {   
        public static void MostrarTextoCentrado(string Texto, ConsoleColor Color = ConsoleColor.White)
        {
            int AnchoDeConsola = WindowWidth;
            int Relleno;
            List<string> Lineas = Texto.Split('\n').ToList();

            ForegroundColor = Color;
            foreach (string Contenido in Lineas)
            {
                Relleno = (AnchoDeConsola - Contenido.Length) / 2;
                SetCursorPosition(Relleno, Console.CursorTop);
                WriteLine(Contenido);
            }
            ResetColor();
        }

        public static void MostrarInstrucciones()
        {
            Clear();
            string Info = @"
                Este juego esta basado en la famosa franquicia de videojuegos 'Pokemon'. 
                Para el funcionamiento de este juego se utiliza como recurso la api pokeApi
                que contiene una base de datos con la informacion de los distintos pokemones,
                items, juegos y generaciones producto de esta saga.  Sin embargo, este juego
                solo hara uso de los pokemones pertenecientes a la primera generacion.
                ";

            string Instrucciones = @"
                El juego consiste en 10 rondas donde deberas enfrentarte a difernetes jefes
                para pasar a la siguiente ronda.
                Al principio podras elegir 3 pokemon aleatorios para utilizar durante el combate
                contra los jefes.
                Al final de cada ronda podras elegir entre subir de nivel de nivel a un pokemon
                o curarlo.
                El juego finalizara en el caso que pierdas contra uno de los jefes o cuando
                hayas derrotado al ultimo de ellos.
            ";

            Interfaz.MostrarTextoCentrado(Info, ConsoleColor.DarkGray);
            Interfaz.MostrarTextoCentrado(Instrucciones, ConsoleColor.White);
            Interfaz.MostrarTextoCentrado("Presiona cualquier tecla para continuar...", ConsoleColor.White);
            ReadKey(true);
        }

        public static string ObtenerNombre()
        {
            Clear();
            string Instruccion = "¿Como te llamas?\nTu nombre debe tener una longitud entre 3 y 10 caracteres (letras o digitos). Ademas, debe comenzar con una letra.\n";
            string Nombre = String.Empty;

            do
            {
                Clear();
                Interfaz.MostrarTextoCentrado(Instruccion, ConsoleColor.Cyan);
                SetCursorPosition((WindowWidth-10)/2, Console.CursorTop);
                Nombre = ReadLine();
            }
            while (Nombre.Length < 3 || Nombre.Length > 10 || !Extra.EsNombreValido(Nombre));

            Clear();
            ResetColor();
            return Nombre;
        }
        
        public static void MostrarMensajePrebatalla()
        {
            string Mensaje = @"
            
   _        __            _                 _ 
  /_\      / / _   _  ___| |__   __ _ _ __ / \
 //_\\    / / | | | |/ __| '_ \ / _` | '__/  /
/  _  \  / /__| |_| | (__| | | | (_| | | /\_/ 
\_/ \_/  \____/\__,_|\___|_| |_|\__,_|_| \/   
                                              
            ";
            Interfaz.MostrarTextoCentrado(Mensaje, ConsoleColor.Cyan);
        }

        public static void MostrarMensajeGanaste()
        {
            string Mensaje = @"
   ___                      _         _ 
  / _ \__ _ _ __   __ _ ___| |_ ___  / \
 / /_\/ _` | '_ \ / _` / __| __/ _ \/  /
/ /_\\ (_| | | | | (_| \__ \ ||  __/\_/ 
\____/\__,_|_| |_|\__,_|___/\__\___\/   
                                        
            ";
            Interfaz.MostrarTextoCentrado("\nVenciste a todos los jefes!\n", ConsoleColor.Green);
            Interfaz.MostrarTextoCentrado(Mensaje, ConsoleColor.Green);
        }
        
        public static void MostrarMensajePerdiste()
        {
            string Mensaje = @"
   ___             _ _     _         _ 
  / _ \___ _ __ __| (_)___| |_ ___  / \
 / /_)/ _ \ '__/ _` | / __| __/ _ \/  /
/ ___/  __/ | | (_| | \__ \ ||  __/\_/ 
\/    \___|_|  \__,_|_|___/\__\___\/    
            ";
            Interfaz.MostrarTextoCentrado("\nTe quedaste sin pokemones!\n", ConsoleColor.Red);
            Interfaz.MostrarTextoCentrado(Mensaje, ConsoleColor.Red);
        }
    }
}