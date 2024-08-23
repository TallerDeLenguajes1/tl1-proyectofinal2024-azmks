using System.IO;
using System.Text.Json;
using System.Threading;
using static System.Console;

namespace Juego
{
    public static class Interfaz
    {   
        public static void MostrarTextoCentrado(string Texto, ConsoleColor FgColor = ConsoleColor.White, ConsoleColor BgColor = ConsoleColor.Black)
        {
            int AnchoDeConsola = BufferWidth;
            int Relleno;
            // Rompe el texto en lineas para poder mostrarlo adecuadamente
            List<string> Lineas = Texto.Split('\n').ToList();

            // Cambio los colores del foreground y del background de la consola por los recibidos o los deja en predeterminado
            // en caso que no se haya especificado los colores
            ForegroundColor = FgColor;
            BackgroundColor = BgColor;

            foreach (string Contenido in Lineas)
            {
                Relleno = (AnchoDeConsola - Contenido.Length) / 2;
                if (Relleno >= BufferWidth || Relleno <= 0) SetCursorPosition(0, Console.CursorTop);
                else SetCursorPosition(Relleno, Console.CursorTop);
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

            MostrarTextoCentrado(Info, ConsoleColor.DarkGray);
            MostrarTextoCentrado(Instrucciones);
            WriteLine();
            MostrarTextoCentrado("IMPORTANTE: Las partidas se guardan automaticamente luego de cada batalla!", ConsoleColor.Yellow);
            MostrarTextoCentrado("Presiona [Enter] para continuar.", ConsoleColor.Yellow);

            ConsoleKey TeclaPresionada = ReadKey(true).Key;
            while (TeclaPresionada != ConsoleKey.Enter) TeclaPresionada = ReadKey(true).Key;
        }

        public static void MostrarGanadores()
        {
            Clear();
            string RutaArchivo = "ganadores.json";

            if (!Extra.Existe(RutaArchivo) || Extra.EstaVacio(RutaArchivo))
            {
                MostrarTextoCentrado("No hay ganadores registrados.");
                MostrarTextoCentrado("¡Apurate y se el primero!");
            }
            else
            {
                try
                {

                    // El tamaño del "cuadro" que tiene la informacion del ganador
                    // Necesario para poder centrar el texto correctamente
                    int relleno = 22;
                    
                    MostrarTextoCentrado("  ---   GANADORES   ---  ", ConsoleColor.White, ConsoleColor.DarkCyan);

                    // Llama al metodo LeerGanadores() de la clase estatica HistorialJson y guarda la lista de ganadores
                    List<Ganador> ListaGanadores = HistorialJson.LeerGanadores();

                    // Varibales auxiliares que sirven para dar formato y estilo
                    string linea = string.Empty.PadRight(relleno, '=');
                    string vacio = string.Empty.PadRight(relleno);
                    ConsoleColor ColorLetra = ConsoleColor.White;
                    ConsoleColor ColorFondo = ConsoleColor.DarkBlue;

                    // Para cada jugador en la lista de ganadores, muestro su informacion correspondiente
                    foreach (Ganador Jugador in ListaGanadores)
                    {
                        ResetColor();
                        MostrarTextoCentrado("");

                        MostrarTextoCentrado(linea, ColorLetra, ColorFondo);
                        MostrarTextoCentrado(vacio, ColorLetra, ColorFondo);

                        // Muestro el nombre del ganador
                        string nombre = $"  Jugador: {Jugador.Nombre}";
                        MostrarTextoCentrado(vacio, ColorLetra, ColorFondo);

                        // Para cada nombre del pokemon en la lista de heroes ganadores, lo muestro
                        foreach (string Pokemon in Jugador.HeroesGanadores)
                        {
                            MostrarTextoCentrado($"  {Pokemon.PadRight(relleno-2)}", ColorLetra, ColorFondo);
                        }

                        MostrarTextoCentrado(vacio, ColorLetra, ColorFondo);
                        MostrarTextoCentrado(linea, ColorLetra, ColorFondo);
                        MostrarTextoCentrado("");
                    }
                                            
                    ResetColor();
                    MostrarTextoCentrado("");
                    
                    // Esperar hastaq ue el jugador presione enter
                    MostrarTextoCentrado("Presiona [Enter] para continuar.", ConsoleColor.Yellow);
                    ConsoleKey TeclaPresionada = ReadKey(true).Key;
                    while (TeclaPresionada != ConsoleKey.Enter) TeclaPresionada = ReadKey(true).Key;
                }
                catch (IOException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al abrir/leer el archivo '{RutaArchivo}': {error.Message}");
                    ResetColor();
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al procesar el archivo '{RutaArchivo}': {error.Message}");
                    ResetColor();
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($"Error al leer ganadores en '{RutaArchivo}': {error.Message}");
                    ResetColor();
                }
            }
        }

        public static string ObtenerNombre()
        {
            Clear();
            string Instruccion = "¿Como te llamas?\nTu nombre debe tener una longitud entre 3 y 10 caracteres (letras o digitos). Ademas, debe comenzar con una letra.\n";
            string Nombre;

            do
            {
                Clear();
                MostrarTextoCentrado(Instruccion, ConsoleColor.Cyan);
                SetCursorPosition((WindowWidth-10)/2, CursorTop);
                Nombre = ReadLine();
            }
            while (Nombre.Length < 3 || Nombre.Length > 10 || !Extra.EsNombreValido(Nombre));

            Clear();
            ResetColor();
            return Nombre;
        }
        
        public static void MostrarMensajePrebatalla()
        {
            Clear();
            string Mensaje = @"
            
   _        __            _                 _ 
  /_\      / / _   _  ___| |__   __ _ _ __ / \
 //_\\    / / | | | |/ __| '_ \ / _` | '__/  /
/  _  \  / /__| |_| | (__| | | | (_| | | /\_/ 
\_/ \_/  \____/\__,_|\___|_| |_|\__,_|_| \/   
                                              
            ";
            MostrarTextoCentrado(Mensaje, ConsoleColor.Cyan);
            WriteLine();
            Thread.Sleep(1500);
        }

        public static void MostrarMensajeGanaste()
        {
            Clear();
            string Mensaje = @"
   ___                      _         _ 
  / _ \__ _ _ __   __ _ ___| |_ ___  / \
 / /_\/ _` | '_ \ / _` / __| __/ _ \/  /
/ /_\\ (_| | | | | (_| \__ \ ||  __/\_/ 
\____/\__,_|_| |_|\__,_|___/\__\___\/   
                                        
            ";
            WriteLine();
            MostrarTextoCentrado(Mensaje, ConsoleColor.Green);
            MostrarTextoCentrado("Venciste a todos los jefes!", ConsoleColor.Green);
            WriteLine();
            Thread.Sleep(1500);
        }
        
        public static void MostrarMensajePerdiste()
        {
            Clear();
            string Mensaje = @"
   ___             _ _     _         _ 
  / _ \___ _ __ __| (_)___| |_ ___  / \
 / /_)/ _ \ '__/ _` | / __| __/ _ \/  /
/ ___/  __/ | | (_| | \__ \ ||  __/\_/ 
\/    \___|_|  \__,_|_|___/\__\___\/    
            ";
            WriteLine();
            MostrarTextoCentrado(Mensaje, ConsoleColor.Red);
            MostrarTextoCentrado("Te quedaste sin pokemones!", ConsoleColor.Red);
            WriteLine();
            Thread.Sleep(1500);
        }
    }
}