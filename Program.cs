using System;

namespace Juego
{
    class Program
    {
        static void Main()
        {
            string Titulo = @"
   ___      _          __                           _     
  / _ \___ | | _____  / /  ___  __ _  ___ _ __   __| |___ 
 / /_)/ _ \| |/ / _ \/ /  / _ \/ _` |/ _ \ '_ \ / _` / __|
/ ___/ (_) |   <  __/ /__|  __/ (_| |  __/ | | | (_| \__ \
\/    \___/|_|\_\___\____/\___|\__, |\___|_| |_|\__,_|___/
                               |___/                      
                ";
            string[] Opciones = {"Nueva Partida", "Cargar Partida", "Como Jugar", "Salir del Juego"};
            Menu Principal = new Menu(Titulo, Opciones);
            Principal.Mostrar();
            int Eleccion = Principal.Ejecutar();
            switch (Eleccion)
            {
                case 0:
                    PartidaJson miPartida = new PartidaJson();
                    miPartida.obtenerNombre();
                    miPartida.obtenerPokemones();
                    miPartida.crearJefes();
                    break;
                case 1:
                    PartidaJson miPartida = new PartidaJson();
                    miPartida.cargarPartida;
                    break;
                case 2:
                    do
                    {
                        Interfaz.mostrarInstrucciones();
                        Eleccion = Principal.Ejecutar();
                    }
                    while (Eleccion == 2);
                    break;
                case 3:
                    Exit();
                    break;
            }
        }
    }
}