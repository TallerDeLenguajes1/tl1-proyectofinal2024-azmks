using static System.Console;
using System.Collections.Generic;

namespace Juego
{
    class Program
    {
        static async Task Main()
        {
            ResetColor();
            string Titulo = @"
   ___      _          __                           _     
  / _ \___ | | _____  / /  ___  __ _  ___ _ __   __| |___ 
 / /_)/ _ \| |/ / _ \/ /  / _ \/ _` |/ _ \ '_ \ / _` / __|
/ ___/ (_) |   <  __/ /__|  __/ (_| |  __/ | | | (_| \__ \
\/    \___/|_|\_\___\____/\___|\__, |\___|_| |_|\__,_|___/
                               |___/                      
            ";

            List<string> Opciones = ["Cargar Personajes", "Ver Instrucciones", "Ver Ganadores","Salir del Juego"];
            Menu Principal = new Menu(Titulo, Opciones);
            int Eleccion;
            
            do
            {
                Eleccion = Principal.Ejecutar(ConsoleColor.Cyan);
                switch (Eleccion)
                {
                    case 0:
                        Personaje PersonajePredet = FabricaDePersonajes.ObtenerPersonajeAleatorio();
                        if (PersonajePredet != null) PersonajePredet.MostrarPersonaje();

                        List<Personaje> lista = await FabricaDePersonajes.GenerarPersonajes(30);
                        PersonajesJson.GuardarPersonajes(lista);
                        foreach (Personaje perso in lista)
                        {
                            WriteLine();
                            perso.MostrarPersonaje();
                            WriteLine();
                        }
                        break;
                    case 1:
                        Interfaz.MostrarInstrucciones();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
            while (Eleccion == 1);

            /*
            Clear();
            List<Personaje> misPersonajes = await FabricaDePersonajes.GenerarPersonajes();
            foreach(Personaje miPersonaje in misPersonajes)
            {
                miPersonaje.MostrarDatos();
                WriteLine();
            }
            */
        }
    }
}