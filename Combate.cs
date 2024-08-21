using System;
using static System.Console;
using System.Collections.Generic;

namespace Juego
{
    /*
    public class Combate
    {
        private Personaje Campeon;
        public List<Personaje> CampeonesLista {get;}
        private Personaje Jefe;
        public List<Personaje> JefesLista {get;}
        public int Ronda {get; set;}
        private Random Aleatorio = new Random();

        public Combate(List<Personaje> champeons, List<Personaje> bosses, int round)
        {
            JefesLista = oponentes;
            CampeonesLista = champeons;
            foreach(Personaje champeon in champeons)
            {
                if (champeon.InfoCaracteristicas.Salud > 0) Campeon = champeon;
            }
            JefesLista = bosses;
            foreach(Personaje boss in bosses)
            {
                if (boss.InfoCaracteristicas.Salud > 0) Jefe = boss;
            }
            Ronda = round;
        }

        public void mostrarBatalla()
        {
            Clear();

            Interfaz.MostrarTextoCentrado($"RONDA {Ronda}", ConsoleColor.Cyan);
            int vidaCampeon = Campeon.InfoCaracteristicas.Salud;
            int vidaJefe = Jefe.InfoCaracteristicas.Salud;

            // Mostrare la informacion de la batalla en verde
            ForegroundColor = ConsoleColor.Green;

            // Muestra la barra de vida del campeon y del jefe, se dibuja una # por cada 5 puntos de vida
            string barraDeVida = string.Empty;
            barraDeVida.PadRight(vidaCampeon/5, "#");
            barraDeVida.PadRight(WindowWidth - vidaJefe/5);
            barraDeVida.PadRight(WndowWidth, "#");
            WriteLine(barraDeVida);

            // Muestra la cantidad de vida del campeon y del jefe
            string cantVidaLinea = $"Vida: {vidaCampeon}";
            string cantVidaJefe = $"Vida: {vidaJefe}";
            cantVidaCampeon.PadRigth(WindowWidth-cantVidaJefe);
            cantVidaLinea += cantVidaJefe;
            WriteLine(cantVidaLinea);
            
            // Muestra los nombres del campeon y del jefe
            string nombreLinea = Campeon.InfoDatos.Nombre;
            nombreLinea.PadRight(WindowWidth-Jefe.InfoDatos.Nombre.Length);
            nombreLinea += Jefe.InfoDatos.Nombre.Length;
            WriteLine(nombreLinea);

            ResetColor();
        }
        
        public int calcularDano(int fuerza, int nivel, int defensa, int elementoAtaque, List<ElementoPokemon> elementosDefensor)
        {
            // Le asigno un numero aleatorio entre 1 y 100 a la efectidad
            int Efectividad = Aleatorio.Next(1, 101);
            int dano = ((fuerza * nivel * Efectividad) - defensa) / 500;

            // Para cada elemento del personaje que recibe el ataque verifico si el elemento del ataque pertenece a sus debilidades
            // y duplico el daño y salgo del bucle (solo necesito duplicar el dano una unica vez) en caso de ser así
            foreach (ElementoPokemon elementoDefensor in elementosDefensor)
            {
                if (InfoDatos.Debilidades.Contains(Elemento))
                {
                    dano *= 2;
                    break;
                }
            }

            // Devuelvo el maximo entre el daño generado y 0, así en caso que el daño sea negativo se devolvera 0
            return Math.Max(0, dano);
        }

        public void cambiarCampeon()
        {
            // Creo una lista de los nombres de los campeones y le inserto aquellos que pueden pelear (tienen una vida mayor que 0)
            List<string> campeonesDisponibles = new List<Personaje>();
            foreach (Personaje posibleCampeon in CampeonesLista)
            {
                if (posibleCampeon.InfoCaracteristicas.Salud > 0)
                {
                    campeonesDisponibles.Add(posibleCampeon.InfoDatos.Nombre);
                }
            }

            // Ejecuto el menu y cambio el campeon al seleccionado
            Menu menuCampeones = new Menu("Seleccione el pokemon que desea utilizar en batalla.", campeonesDisponibles);
            int eleccion = menuCampeones.Ejecutar(ConsoleColor.Green);
            Campeon = campeonesDisponibles[eleccion];
        }
        
        public void miTurno()
        {
            do
            {     
                // Muestro el estado actual de la batalla
                mostrarBatalla();

                // Guardo los valores del fuerza, nivel y defensa del campeon y del jefe
                int fuerzaCampeon = Campeon.InfoCaracteristicas.Fuerza;
                int nivelCampeon = Campeon.InfoCaracteristicas.Nivel;
                int defensaCampeon = Campeon.InfoCaracteristicas.Defensa;
                List<ElementoPokemon> DebilidadesJefe = Jefe.InfoDatos.Debilidades;

                // Genero una lista de strings que representan los posibles ataques que el jugador puede seleccionar
                // Puede atacar cuerpo a cuerpo, realizar un ataque de alguno de sus tipos o incluso cambiar de pokemon
                List<string> OpcionesBatalla = ["Cambiar Pokemon", "Ataque Cuerpo a Cuerpo"];
                foreach (TipoPokemon Elemento in Campeon.InfoDatos.Tipos)
                {
                    OpcionesBatalla.Add($"Ataque Tipo {Elemento}");
                }

                // Muestro al usuario un menu
                Menu menuBatalla = new Menu("Tu Turno!", OpcionesBatalla);
                int eleccion = menuBatalla.Ejecutar();

                switch (eleccion)
                {
                    case 0:
                        cambiarCampeon();
                        break;
                    case 1:
                        int dano = calcularDano(fuerzaCampeon, nivelCampeon, defensaCampeon, ElementoPokemon.Ninguno, DebilidadesJefe);
                        Jefe.InfoCaracteristicas.Salud -= dano;
                        break;
                    case 2:
                        int dano = calcularDano(fuerzaCampeon, nivelCampeon, defensaCampeon, Campeon.InfoDatos.Tipos[0], DebilidadesJefe);
                        Jefe.InfoCaracteristicas.Salud -= dano;
                        break;
                    case 3:
                        int dano = calcularDano(fuerzaCampeon, nivelCampeon, defensaCampeon, Campeon.InfoDatos.Tipos[1], DebilidadesJefe);
                        Jefe.InfoCaracteristicas.Salud -= dano;
                        break;
                }
            }
            // Mientras la opcion seleccionado sea la de cambiar el campeon, cambio de campeon y vuelvo a mostrar la pantalla de batalla
            // para preguntor al jugador la accion que desea ejecutar
            while (eleccion == 0);
            
            // Espera 3 segundos antes de continuar
            System.Threading.Thread.Sleep(3000);
        }

        public void turnoJefe()
        {
            // Guardo las caracteristicas del jefe y las debilidades del campeon en distintas variables
            int fuerzaJefe = Jefe.InfoCaracteristicas.Fuerza;
            int nivelJefe = Jefe.InfoCaracteristicas.Nivel;
            int defensaJefe = Jefe.InfoCaracteristicas.Defensa;
            int cantElementosJefe = Jefe.InfoDatos.Tipos.Count;
            List<ElementoPokemon> DebilidadesCampeon = Campeon.InfoDatos.Debilidades;

            // Obtengo un numero aleatoria que utilizare para determinar el tipo de ataque que hará el jefe
            int indiceElemento = Aleatorio.Next(0, cantElementosJefe+1);
            int dano;
            string ataque;

            // Si el jefe solo tiene un unico tipo entonces solo tiene 2 opciones de ataque
            if (cantElementosJefe == 1)
            {
                switch (indiceElemento)
                {
                    case 0:
                        dano = calcularDano(fuerzaJefe, nivelJefe, defensaJefe, Jefe.InfoDatos.Tipos[indiceElemento], DebilidadesCampeon);
                        Campeon.InfoCaracteristicas.Salud -= dano;
                        ataque = $"Ataque Tipo {Jefe.InfoDatos.Tipos[indiceElemento]}";
                        break;
                    case 1:
                        dano = calcularDano(fuerzaJefe, nivelJefe, defensaJefe, ElementoPokemon.Ninguno, DebilidadesCampeon);
                        Campeon.InfoCaracteristicas.Salud -= dano;
                        ataque = "Ataque Cuerpo a Cuerpo";
                        break;
                }
            }
            // Si el jefe solo tiene 2 tipos entonces tiene 3 opciones de ataque
            else if (cantElementosJefe == 2)
            {
                switch (indiceElemento)
                {
                    case 0:
                        dano = calcularDano(fuerzaJefe, nivelJefe, defensaJefe, Jefe.InfoDatos.Tipos[indiceElemento], DebilidadesCampeon);
                        Campeon.InfoCaracteristicas.Salud -= dano;
                        ataque = $"Ataque Tipo {Jefe.InfoDatos.Tipos[indiceElemento]}";
                        break;
                    case 1:
                        dano = calcularDano(fuerzaJefe, nivelJefe, defensaJefe, Jefe.InfoDatos.Tipos[indiceElemento], DebilidadesCampeon);
                        Campeon.InfoCaracteristicas.Salud -= dano;
                        ataque = $"Ataque Tipo {Jefe.InfoDatos.Tipos[indiceElemento]}";
                        break;
                        
                    case 2:
                        dano = calcularDano(fuerzaJefe, nivelJefe, defensaJefe, ElementoPokemon.Ninguno, DebilidadesCampeon);
                        Campeon.InfoCaracteristicas.Salud -= dano;
                        ataque = "Ataque Cuerpo a Cuerpo";
                        break;
                }
            }

            // Luego del ataque del jefe, muestro los resultados
            mostrarBatalla();
            Interfaz.MostrarTextoCentrado($"{Jefe.InfoDatos.Nombre} utilizo "+ataque, ConsoleColor.Red);

            // Espera 3 segundos antes de continuar
            System.Threading.Thread.Sleep(3000);
        }
        public void siguienteCampeon()
        {
            // De la lista de campeones cambio al proximo campeon que pueda pelear (tenga vida mayor que 0)
            foreach (Personaje posibleCampeon in CampeonesLista)
            {
                if (posibleCampeon.InfoCaracteristicas.Salud > 0)
                {
                    Campeon = posibleCampeon;
                    break;
                }
            }
        }

        public void siguienteJefe()
        {
            // De la lista de jefes cambio al proximo jefe que pueda pelear (tenga vida mayor que 0)
            foreach (Personaje posibleJefe in JefesLista)
            {
                if (posibleJefe.InfoCaracteristicas.Salud > 0)
                {
                    Jefe = posibleJefe;
                    break;
                }
            }
        }
        
        public int ejecutarRondaActual()
        {
            // Hacer el combate por turnos mientras tanto el jefe como el campeon tengan vida para seguir peleando
            do
            {
                miTurno();
                turnoJefe();
            }
            while (Campeon.InfoCaracteristicas.Salud > 0 && Jefe.InfoCaracteristicas.Salud > 0);

            Clear();
            if (Campeon.InfoCaracteristicas.Salud > 0 && Jefe.InfoCaracteristicas.Salud <= 0)
            {
                // Pasa a la siguiente ronda
                if (Ronda <= 10)
                {    
                    Interfaz.MostrarTextoCentrado($"Felicidades!\nPasaste a la Ronda {Ronda}\n", ConsoleColor.Green);
                    // Cura al campeon 10 puntos o reestablece su vida al maximo en caso de tener más de 90 puntos de vida
                    if (Campeon.InfoCaracteristicas.Salud <= 90)
                    {
                        Campeon.InfoCaracteristicas.Salud += 10;
                        Interfaz.MostrarTextoCentrado($"{Campeon.InfoDatos.Nombre} gano 10 puntos de vida.", ConsoleColor.White);
                    }
                    else if (Campeon.InfoCaracteristicas.Salud < 100)
                    {
                        Campeon.InfoCaracteristicas.Salud = 100;
                        Interfaz.MostrarTextoCentrado($"{Campeon.InfoDatos.Nombre} restablecio su vida al maximo.", ConsoleColor.White);
                    }

                    // Si el campeon no tiene su fuerza o su defensa al maximo, las aumenta en 1 punto
                    if (Campeon.InfoCaracteristicas.Fuerza <= 9)
                    {
                        Campeon.InfoCaracteristicas.Fuerza += 1;
                        Interfaz.MostrarTextoCentrado($"{Campeon.InfoDatos.Nombre} aumento su fuerza en 1 punto.", ConsoleColor.White);
                    }
                    if (Campeon.InfoCaracteristicas.Defensa <= 9)
                    {
                        Campeon.InfoCaracteristicas.Defensa += 1;
                        Interfaz.MostrarTextoCentrado($"{Campeon.InfoDatos.Nombre} aumento su defensa en 1 punto.", ConsoleColor.White);
                    }

                    Interfaz.MostrarTextoCentrado("\nPresione Cualquier tecla para continuar...", ConsoleColor.DarkGray);
                    ReadKey(true);
                    JefesLista.Remove(Jefe);
                    siguienteJefe();
                }
            }
            else
            {
                Interfaz.MostrarTextoCentrado($"Tu pokemon: {Campeon.InfoDatos.Nombre} quedo fuera de combate.\n", ConsoleColor.Yellow);
                // Cura al Jefe 10 puntos o reestablece su vida al maximo en caso de tener más de 90 puntos de vida
                if (Jefe.InfoCaracteristicas.Salud <= 90)
                {
                    Jefe.InfoCaracteristicas.Salud += 10;
                    Interfaz.MostrarTextoCentrado($"{Jefe.InfoDatos.Nombre} gano 10 puntos de vida.", ConsoleColor.White);
                }
                else if (Jefe.InfoCaracteristicas.Salud < 100)
                {
                    Jefe.InfoCaracteristicas.Salud = 100;
                    Interfaz.MostrarTextoCentrado($"{Jefe.InfoDatos.Nombre} restablecio su vida al maximo.", ConsoleColor.White);
                }

                // Si el Jefe no tiene su fuerza o su defensa al maximo, las aumenta en 1 punto
                if (Jefe.InfoCaracteristicas.Fuerza <= 9)
                {
                    Jefe.InfoCaracteristicas.Fuerza += 1;
                    Interfaz.MostrarTextoCentrado($"{Jefe.InfoDatos.Nombre} aumento su fuerza en 1 punto.", ConsoleColor.White);
                }
                if (Jefe.InfoCaracteristicas.Defensa <= 9)
                {
                    Jefe.InfoCaracteristicas.Defensa += 1;
                    Interfaz.MostrarTextoCentrado($"{Jefe.InfoDatos.Nombre} aumento su defensa en 1 punto.", ConsoleColor.White);
                }
                
                Interfaz.MostrarTextoCentrado("\nPresione Cualquier tecla para continuar...", ConsoleColor.DarkGray);
                ReadKey(true);
                CampeonesLista.Remove(Campeon);
                siguienteCampeon();
            }
        
            return Ronda;
        }
    }
    */
}