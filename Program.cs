using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

            string[] Opciones = {"Nueva Partida", "Cargar Partida", "Salir del Juego"};
            Menu Principal = new Menu(Titulo, Opciones);
            Principal.Mostrar();
            int Eleccion = Principal.Ejecutar();
            PartidaJson miPartida = new PartidaJson();

            switch (Eleccion)
            {
                case 0:
                    miPartida.obtenerNombre();
                    Interfaz.mostrarInstrucciones();
//                    miPartida.obtenerPokemones();
//                    miPartida.crearJefes();
                    break;
                case 1:
//                    miPartida.cargarPartida();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }


        }
    }
}