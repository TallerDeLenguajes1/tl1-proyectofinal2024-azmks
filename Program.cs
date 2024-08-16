using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static System.Console;

namespace Juego
{
    public class Program
    {
        public static void Main()
        {
            string Titulo = @"
   ___      _          __                           _     
  / _ \___ | | _____  / /  ___  __ _  ___ _ __   __| |___ 
 / /_)/ _ \| |/ / _ \/ /  / _ \/ _` |/ _ \ '_ \ / _` / __|
/ ___/ (_) |   <  __/ /__|  __/ (_| |  __/ | | | (_| \__ \
\/    \___/|_|\_\___\____/\___|\__, |\___|_| |_|\__,_|___/
                               |___/                      
            ";

            string[] Opciones = {"Nueva Partida", "Cargar Partida", "Ver Instrucciones", "Ver Ganadores", "Salir del Juego"};
            Menu Principal = new Menu(Titulo, Opciones);
            int Eleccion;
            PartidaJson miPartida = new PartidaJson();

            do
            {
                Eleccion = Principal.Ejecutar(ConsoleColor.Cyan);
                switch (Eleccion)
                {
                    case 0:
                        Interfaz.mostrarInstrucciones();
                        string nombrePartida = Interfaz.obtenerNombre();

                        FabricaDePersonajes Fabrica = new FabricaDePersonajes();
                        Interfaz.mostrarTextoCentrado("GENERANDO TUS POKEMONES", ConsoleColor.Cyan);
                        List<Personaje> misCampeones = FabricaDePersonajes.generarPersonajesIniciales();            
                        System.Threading.Thread.Sleep(3000);
                        Interfaz.mostrarTextoCentrado("GENERANDO JEFES...", ConsoleColor.Cyan);
                        List<Personaje> misJefes = FabricaDePersonajes.generarPersonajesIniciales();
                        System.Threading.Thread.Sleep(3000);

                        miPartida.nuevaPartida(nombrePartida, misCampeones, misJefes);
                        break;
                    case 1:
                        miPartida.cargarPartida();
                        break;
                    case 2:
                        Interfaz.mostrarInstrucciones();
                        break;
                    case 3:
                        List<Ganador> misGanadores = HistorialJson.LeerGanadores("ganadores.json");
                        foreach (Ganador winner in misGanadores)
                        {
                            Interfaz.mostrarTextoCentrado($"{winner.Nombre.ToUpper()}:", ConsoleColor.Green);
                            foreach (string campeon in winner.CampeonesGanadores)
                            {
                                Interfaz.mostrarTextoCentrado(campeon, ConsoleColor.White);
                            }
                        }
                        Interfaz.mostrarTextoCentrado("\nPresione cualquier tecla para continuar...", ConsoleColor.White);
                        ReadKey();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
            while (Eleccion == 2 || Eleccion == 3);

            Menu menuEntreRondas = new Menu("¿Que desea hacer?", ["Continuar", "Guardar partida y continuar", "Salir y guardar", "Salir"]);
            Combate juego = new Combate(miPartida.Campeones, miPartida.Jefes, miPartida.RondaActual);
            int proxRonda;
            do
            {
                proxRonda = juego.ejecutarRondaActual();
                if (proxRonda == juego.Ronda)
                {
                    if (juego.CampeonesLista.Count <= 0)
                    {
                        Interfaz.mostrarMensajePerdiste();
                        // Espera 3 segundos antes de continuar
                        System.Threading.Thread.Sleep(3000);
                        Environment.Exit(0);
                    }
                    else 
                    {      
                        Interfaz.mostrarTextoCentrado($"Perdiste el combate!\nTu proxima ronda es: {proxRonda}", ComsoleColor.Red);
                        Eleccion = menuEntreRondas.Ejecutar(ConsoleColor.Red);

                        switch (Eleccion)
                        {
                            case 0:
                                // Espera 3 segundos antes de continuar
                                System.Threading.Thread.Sleep(3000);
                                break;
                            case 1:
                                miPartida.GuardarPartida(juego.Ronda, juego.CampeonesLista, juego.JefesLista);
                                // Espera 3 segundos antes de continuar
                                System.Threading.Thread.Sleep(3000);
                                break;
                            case 2:
                                miPartida.GuardarPartida(juego.Ronda, juego.CampeonesLista, juego.JefesLista);
                                // Espera 3 segundos antes de continuar
                                System.Threading.Thread.Sleep(3000);
                                Environment.Exit(0);
                                break;
                            case 3:
                                // Espera 3 segundos antes de continuar
                                System.Threading.Thread.Sleep(3000);
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
                else if (proxRonda <= 10)
                {
                    Interfaz.mostrarTextoCentrado($"Ganaste el combate!\nTu proxima ronda es: {proxRonda}", ComsoleColor.Red);
                    Eleccion = menuEntreRondas.Ejecutar(ConsoleColor.Red);

                    switch (Eleccion)
                    {
                        case 0:
                            break;
                        case 1:
                            miPartida.GuardarPartida(juego.Ronda, juego.CampeonesLista, juego.JefesLista);
                            break;
                        case 2:
                            miPartida.GuardarPartida(juego.Ronda, juego.CampeonesLista, juego.JefesLista);
                            Environment.Exit(0);
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    Interfaz.mostrarMensajeGanaste();
                    HistorialJson.GuardarGanador(miPartida.Nombre, "ganadores.json");
                    // Espera 3 segundos antes de continuar
                    System.Threading.Thread.Sleep(3000);
                    Environment.Exit(0);
                }
            }
            while (proxRonda <= 10);

        }
    }
}