using static System.Console;
using System.Threading;
using System.Collections.Generic;

namespace Juego
{
    class Program
    {
        static async Task Main()
        {
            string Titulo = @"
   ___      _          __                           _     
  / _ \___ | | _____  / /  ___  __ _  ___ _ __   __| |___ 
 / /_)/ _ \| |/ / _ \/ /  / _ \/ _` |/ _ \ '_ \ / _` / __|
/ ___/ (_) |   <  __/ /__|  __/ (_| |  __/ | | | (_| \__ \
\/    \___/|_|\_\___\____/\___|\__, |\___|_| |_|\__,_|___/
                               |___/                      
            ";

            List<string> Opciones = ["Comenzar Nuevo Juego", "Cargar Juego","Ver Instrucciones", "Ver Ganadores","Salir del Juego"];
            Menu Principal = new Menu(Titulo, Opciones);
            int Eleccion;

            // Variable que guarda la partida que se esta jugando actualmente, la inicializo en null
            Partida MiPartida = null;

            // Bandera que cheque si hay una partida cargada o no
            bool PartidaCargada = false;
            
            do
            {
                Eleccion = Principal.Ejecutar(ConsoleColor.Cyan);
                switch (Eleccion)
                {
                    case 0:
                        // Obtengo el nombre, los heroes y los jefes para la nueva partida
                        string NuevoNombre = Interfaz.ObtenerNombre();
                        List<Personaje> NuevosHeroes = await FabricaDePersonajes.GenerarPersonajes();
                        List<Personaje> NuevosJefes = await FabricaDePersonajes.GenerarPersonajes();

                        //Creo la nueva partida
                        MiPartida = new Partida(NuevoNombre, NuevosHeroes, NuevosJefes);

                        // Guardo la partida creada
                        Clear();
                        Interfaz.MostrarTextoCentrado($"Por favor espere mientras se guarda la nueva partida.");
                        HistorialJson.GuardarPartida(MiPartida);
                        Thread.Sleep(2000);

                        PartidaCargada = true;
                        break;
                    case 1:
                        // Si la carpeta de partidas esta vacia, no hay partidas existentes, de lo contrario intento cargar una partida
                        if (Extra.EsCarpetaVacia(@"Partidas\"))
                        {
                            Clear();
                            WriteLine();
                            Interfaz.MostrarTextoCentrado("Todavia no hay partidas guardadas.");
                            // Espero 2 segundos antes de continuar a la siguiente batalla.
                            Thread.Sleep(200);
                        }
                        else
                        {
                            // Obtengo una lista con los nombre de los archivos en la carpeta partidas
                            List<string> ArchivosPartidas = HistorialJson.ObtenerListaPartidas();

                            if (Extra.TieneElementos<string>(ArchivosPartidas))
                            {
                                // Agrego una opcion para volver al menu principal
                                ArchivosPartidas.Add("Volver");
                                
                                // Muestro el listado de jugadores que tienen partida guardada
                                Menu MenuPartidas = new Menu("Seleccione la partida que desea cargar\n", ArchivosPartidas);
                                int indicePartidas = MenuPartidas.Ejecutar(ConsoleColor.Yellow);

                                // A menos que se elija la opcion 'Volver', cargo la partida en
                                // la ruta elegida usando el metodo CargarPartida de la clase estatica HistorialJson.
                                if (indicePartidas != ArchivosPartidas.Count-1) 
                                {
                                    MiPartida = HistorialJson.CargarPartida(ArchivosPartidas[indicePartidas]);
                                    PartidaCargada = true;
                                }
                                
                                break;
                            }
                            else
                            {
                                Clear();
                                WriteLine();
                                Interfaz.MostrarTextoCentrado("Todavia no hay partidas guardadas.");

                                // Espero 2 segundos antes de continuar a la siguiente batalla.
                                Thread.Sleep(2000);
                            }
                        }
                        break;
                    case 2:
                        Interfaz.MostrarInstrucciones();
                        break;
                    case 3:
                        Interfaz.MostrarGanadores();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
            while (Eleccion ==  2 || Eleccion == 3 || !PartidaCargada);


            // En caso que no se haya salido del juego, ya sea para comenzar un nuevo juego o cargar uno
            // muestro los heroes de la partida y comienzan las batallas
            MiPartida.MostrarHeroes();
            bool GanoCombate;
            string Jefe;

            do
            {
                // El metodo combate devuelve true si se gano el combate y false si se perdio
                GanoCombate = MiPartida.Combate();

                if (GanoCombate)
                {
                    // Aumento el numero de la ronda de la partida y obtengo el siguiente jefe
                    MiPartida.Ronda++;
                    Jefe = MiPartida.JefeActual.InfoDatos.Nombre;
                    // Avanzo al siguiente jefe que pueda pelear
                    MiPartida.SiguienteJefe();
                    Clear();

                    // Mientras la siguiente ronda sea menor o igual que 10 (la cant de rondas del juego) continuo a la siguiente batalla
                    if (MiPartida.Ronda <= 10)
                    {   
                        // Muestro el nombre del jefe vencido y a que numero ronda se continuara
                        WriteLine();

                        // El heroe vencedor se ve beneficiado con recompensas
                        MiPartida.Recompensas(Jefe);
                        
                        // Espero 2 segundos antes de continuar a la siguiente batalla.
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        // Muestro mensaje 'Ganaste', creo un nuevo ganador con la info de la partida y lo guardo
                        Interfaz.MostrarMensajeGanaste();

                        Ganador NuevoGanador = new Ganador(MiPartida.Nombre, MiPartida.Heroes);
                        HistorialJson.GuardarGanador(NuevoGanador);
                        
                        // Muestro los ganadores
                        Interfaz.MostrarGanadores();
                        break;
                    }

                }
                else if (MiPartida.Heroes.Count > 0)
                {
                    // Muestro el nombrepokemon fuera de combate, cambio al siguiente y muestro su nombre
                    Clear();
                    Interfaz.MostrarTextoCentrado($"{MiPartida.HeroeActual.InfoDatos.Nombre} quedo fuera de combate.");
                    MiPartida.SiguienteHeroe();
                    Interfaz.MostrarTextoCentrado($"{MiPartida.HeroeActual.InfoDatos.Nombre} entra a la batalla.");

                    // El jefe vencedor se ve beneficiado subiendo un nivel si es que no esta en su maximo nivel
                    if (MiPartida.JefeActual.InfoCaracteristicas.Nivel < 10) MiPartida.JefeActual.InfoCaracteristicas.Nivel++;

                    // Espero 2 segundos antes de continuar a la siguiente batalla.
                    Thread.Sleep(2000);
                }
                else
                {
                    // Muestro mensaje 'perdiste'
                    Interfaz.MostrarMensajePerdiste();
                }

                // Antes de continuar, guardo la partida invocando al metodo GuardarPartida() de la clase estatica HistorialJson
                Clear();
                Interfaz.MostrarTextoCentrado($"Por favor espere mientras se guardan los avances de la partida.");
                HistorialJson.GuardarPartida(MiPartida);
                Thread.Sleep(2000);
            }
            while (MiPartida.Ronda <= 10 && MiPartida.Heroes.Count > 0);
        }
    }
}