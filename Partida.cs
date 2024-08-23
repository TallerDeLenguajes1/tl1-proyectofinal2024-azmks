using System.Collections.Generic;
using System.Threading;
using static System.Console;
using System.Text.Json.Serialization;

namespace Juego
{
    public class Partida
    {
        public string Nombre {get; set;}
        public int Ronda {get; set;}
        public List<Personaje> Heroes {get; set;}
        public List<Personaje> Jefes {get; set;}
        public Personaje HeroeActual {get; set;}
        public Personaje JefeActual {get; set;}
        [JsonIgnore]
        private readonly Random Aleatorio = new Random();

        public Partida(string Nombre, List<Personaje> Heroes, List<Personaje> Jefes)
        {
            this.Nombre = Nombre;
            this.Ronda = 1;
            this.Heroes = Heroes;
            this.Jefes = Jefes;

            // Si las listas recibidas tienen por lo menos un elemento, asigno el primer elemento al heroe actual y al jefe
            if (Extra.TieneElementos<Personaje>(Heroes)) HeroeActual = Heroes[0];
            if (Extra.TieneElementos<Personaje>(Jefes)) JefeActual = Jefes[0];
        }
        
        public bool Combate()
        {
            Interfaz.MostrarMensajePrebatalla();

            do
            {
                MiTurno();
                if (JefeActual.InfoCaracteristicas.Salud < 0) break;
                TurnoJefe();
                if (HeroeActual.InfoCaracteristicas.Salud < 0) break;
            }
            while (HeroeActual.InfoCaracteristicas.Salud > 0 && JefeActual.InfoCaracteristicas.Salud > 0);

            // Retorna true si se gano el combate (El hero sobrevivio) o false si se perdio el combate (El heroe no sobrevivio)
            return HeroeActual.InfoCaracteristicas.Salud > 0;
        }

        public int CalcularDano(int Fuerza, int Nivel, int Defensa, ElementoPokemon ElementoAtaque, List<ElementoPokemon> ElementoDefensa)
        {
            // Le asigno un numero aleatorio entre 33 y 100 a la precision
            int Precision = Aleatorio.Next(33, 101);
            // Segun el elemento del Ataque y los elementos del personaje defensor calculo la efectividad
            float Efectividad = CalcularEfectividad(ElementoAtaque, ElementoDefensa);
            float Dano = ((Fuerza * Nivel * Precision) - Defensa) * Efectividad / 150;
            
            // Devuelvo el maximo entre el daño generado y 0, así en caso que el daño sea negativo se devolvera 0
            return Math.Max(0, (int) Dano);
        }

        public void CambiarHeroe()
        {
            // Si quedan Personajes en la lista de heroes, permito realizar el cambio
            if (Heroes.Count > 0)
            {
                // Creo una lista de los nombres de los Heroes y le inserto aquellos que pueden pelear (tienen una vida mayor que 0)
                List<Personaje> HeroesDisponibles = new List<Personaje>();
                List<string> OpcionesHeroes = new List<string>();
                foreach (Personaje PosibleHeroe in Heroes)
                {
                    if (PosibleHeroe.InfoCaracteristicas.Salud > 0)
                    {
                        HeroesDisponibles.Add(PosibleHeroe);
                        OpcionesHeroes.Add($"{PosibleHeroe.InfoDatos.Nombre} ({String.Join( ", " ,PosibleHeroe.InfoDatos.Tipos)})");
                    }
                }

                // Ejecuto el menu y cambio el campeon al seleccionado
                Menu menuHeroes = new Menu("Seleccione el pokemon que desea utilizar en batalla.\n", OpcionesHeroes);
                int Eleccion = menuHeroes.Ejecutar(ConsoleColor.Green);
                HeroeActual = HeroesDisponibles[Eleccion];
            }
            else
            {
                Interfaz.MostrarTextoCentrado("Te quedaste sin heroes!", ConsoleColor.Red);
            }
        }

        public void MiTurno()
        {
            // Espera 1 segundo antes de continuar
            Thread.Sleep(1000);

            int Eleccion;
            do
            {     
                // Muestro el estado actual de la batalla
                MostrarBatalla();

                // Guardo los valores del Fuerza, Nivel y Defensa del campeon y del jefe
                int FuerzaHeroe = HeroeActual.InfoCaracteristicas.Fuerza;
                int NivelHeroe = HeroeActual.InfoCaracteristicas.Nivel;
                int DefensaHeroe = HeroeActual.InfoCaracteristicas.Defensa;
                List<ElementoPokemon> ElementosJefe = JefeActual.InfoDatos.Tipos;

                // Genero una lista de strings que representan los posibles Ataques que el jugador puede seleccionar
                // Puede atacar cuerpo a cuerpo, realizar un Ataque de alguno de sus tipos o incluso cambiar de pokemon
                List<string> OpcionesBatalla = ["Salir del Juego", "Cambiar Pokemon", "Ver Estadisticas Heroe", "Ver Estadisticas Jefe","Ataque Cuerpo a Cuerpo"];
                foreach (ElementoPokemon Elemento in HeroeActual.InfoDatos.Tipos)
                {
                    OpcionesBatalla.Add($"Ataque Tipo {Elemento}");
                }

                // Muestro al usuario un menu
                Menu MenuBatalla = new Menu("Tu Turno!\n", OpcionesBatalla);
                Eleccion = MenuBatalla.EjecutarBatalla(this);

                // Variable que guarda la tecla presionada en caso de necesitarla
                ConsoleKey TeclaPresionada;

                // Creo una variable para guardar el daño
                int Dano;

                switch (Eleccion)
                {
                    case 0:
                        WriteLine();
                        Interfaz.MostrarTextoCentrado("¿Esta seguro que desea salir del juego?", ConsoleColor.DarkGray);
                        Interfaz.MostrarTextoCentrado("Advertencia: Todos los avances de la batalla actual se perderan.", ConsoleColor.DarkGray);
                        WriteLine();

                        Interfaz.MostrarTextoCentrado("Presiona [Enter] para salir del juego", ConsoleColor.DarkGray);
                        Interfaz.MostrarTextoCentrado("Presiona cualquier otra tecla para continuar", ConsoleColor.DarkGray);

                        
                        TeclaPresionada = ReadKey(true).Key;
                        if (TeclaPresionada == ConsoleKey.Enter) Environment.Exit(0);

                        break;
                    case 1:
                        CambiarHeroe();
                        break;
                    case 2:
                        WriteLine();
                        HeroeActual.MostrarPersonaje();

                        WriteLine();
                        Interfaz.MostrarTextoCentrado("Presiona [Enter] para continuar la partida.", ConsoleColor.Yellow);
                        TeclaPresionada = ReadKey(true).Key;
                        while (TeclaPresionada != ConsoleKey.Enter) TeclaPresionada = ReadKey(true).Key;

                        break;
                    case 3:
                        WriteLine();
                        JefeActual.MostrarPersonaje();

                        WriteLine();
                        Interfaz.MostrarTextoCentrado("Presiona [Enter] para continuar la partida.", ConsoleColor.Yellow);
                        TeclaPresionada = ReadKey(true).Key;

                        while (TeclaPresionada != ConsoleKey.Enter) TeclaPresionada = ReadKey(true).Key;
                        break;
                    case 4:
                        Dano = CalcularDano(FuerzaHeroe, NivelHeroe, DefensaHeroe, ElementoPokemon.Ninguno, ElementosJefe);
                        JefeActual.InfoCaracteristicas.Salud -= Dano;

                        WriteLine();
                        Interfaz.MostrarTextoCentrado($"{HeroeActual.InfoDatos.Nombre} utilizo Ataque Cuerpo a Cuerpo e infligio {Dano} de Dano", ConsoleColor.Green);
                        break;
                    case 5:
                        Dano = CalcularDano(FuerzaHeroe, NivelHeroe, DefensaHeroe, HeroeActual.InfoDatos.Tipos[0], ElementosJefe);
                        JefeActual.InfoCaracteristicas.Salud -= Dano;
                        
                        WriteLine();
                        Interfaz.MostrarTextoCentrado($"{HeroeActual.InfoDatos.Nombre} utilizo Ataque Tipo {HeroeActual.InfoDatos.Tipos[0]} e infligio {Dano} de Dano", ConsoleColor.Green);
                        break;
                    case 6:
                        Dano = CalcularDano(FuerzaHeroe, NivelHeroe, DefensaHeroe, HeroeActual.InfoDatos.Tipos[1], ElementosJefe);
                        JefeActual.InfoCaracteristicas.Salud -= Dano;

                        WriteLine();
                        Interfaz.MostrarTextoCentrado($"{HeroeActual.InfoDatos.Nombre} utilizo Ataque Tipo {HeroeActual.InfoDatos.Tipos[1]} e infligio {Dano} de Dano", ConsoleColor.Green);
                        break;
                }
            }
            while (Eleccion < 4);
            // Mientras la opcion seleccionado sea la de cambiar el heroe, cambio de heroe y vuelvo a mostrar la pantalla de batalla
            // para preguntor al jugador la accion que desea ejecutar
        }

        public void TurnoJefe()
        {
            // Espera 1 segundo antes de continuar
            Thread.Sleep(1000);
            // Guardo las caracteristicas del jefe y las debilidades del campeon en distintas variables
            int FuerzaJefe = JefeActual.InfoCaracteristicas.Fuerza;
            int NivelJefe = JefeActual.InfoCaracteristicas.Nivel;
            int DefensaJefe = JefeActual.InfoCaracteristicas.Defensa;
            int CantElementosJefe = JefeActual.InfoDatos.Tipos.Count;
            List<ElementoPokemon> ElementosHeroe = HeroeActual.InfoDatos.Tipos;

            // Obtengo un numero aleatoria que utilizare para determinar el tipo de Ataque que hará el jefe
            int indiceElemento = Aleatorio.Next(0, CantElementosJefe+1);
            int Dano = 0;
            string Ataque = "Ataque";

            // Si el jefe solo tiene un unico tipo entonces solo tiene 2 opciones de Ataque
            if (CantElementosJefe == 1)
            {
                switch (indiceElemento)
                {
                    case 0:
                        Dano = CalcularDano(FuerzaJefe, NivelJefe, DefensaJefe, JefeActual.InfoDatos.Tipos[indiceElemento], ElementosHeroe);
                        HeroeActual.InfoCaracteristicas.Salud -= Dano;
                        Ataque = $"Ataque Tipo {JefeActual.InfoDatos.Tipos[indiceElemento]}";
                        break;
                    case 1:
                        Dano = CalcularDano(FuerzaJefe, NivelJefe, DefensaJefe, ElementoPokemon.Ninguno, ElementosHeroe);
                        HeroeActual.InfoCaracteristicas.Salud -= Dano;
                        Ataque = "Ataque Cuerpo a Cuerpo";
                        break;
                }
            }
            // Si el jefe solo tiene 2 tipos entonces tiene 3 opciones de Ataque
            else if (CantElementosJefe == 2)
            {
                switch (indiceElemento)
                {
                    case 0:
                        Dano = CalcularDano(FuerzaJefe, NivelJefe, DefensaJefe, JefeActual.InfoDatos.Tipos[indiceElemento], ElementosHeroe);
                        HeroeActual.InfoCaracteristicas.Salud -= Dano;
                        Ataque = $"Ataque Tipo {JefeActual.InfoDatos.Tipos[indiceElemento]}";
                        break;
                    case 1:
                        Dano = CalcularDano(FuerzaJefe, NivelJefe, DefensaJefe, JefeActual.InfoDatos.Tipos[indiceElemento], ElementosHeroe);
                        HeroeActual.InfoCaracteristicas.Salud -= Dano;
                        Ataque = $"Ataque Tipo {JefeActual.InfoDatos.Tipos[indiceElemento]}";
                        break;
                        
                    case 2:
                        Dano = CalcularDano(FuerzaJefe, NivelJefe, DefensaJefe, ElementoPokemon.Ninguno, ElementosHeroe);
                        HeroeActual.InfoCaracteristicas.Salud -= Dano;
                        Ataque = "Ataque Cuerpo a Cuerpo";
                        break;
                }
            }

            // Luego del Ataque del jefe, muestro los resultados
            MostrarBatalla();
            WriteLine();
            Interfaz.MostrarTextoCentrado($"{JefeActual.InfoDatos.Nombre} utilizo {Ataque} e infligio {Dano} de Dano", ConsoleColor.Red);
        }

        public void MostrarBatalla()
        {
            Clear();

            Interfaz.MostrarTextoCentrado($"RONDA {Ronda}", ConsoleColor.Green);
            int vidaCampeon = HeroeActual.InfoCaracteristicas.Salud;
            int vidaJefe = JefeActual.InfoCaracteristicas.Salud;

            // Mostrare la informacion de la batalla en verde
            ForegroundColor = ConsoleColor.Green;

            // Muestra la cantidad de vida del campeon y del jefe
            ForegroundColor = ConsoleColor.Green;
            string lineaPokemon = $"{HeroeActual.InfoDatos.Nombre} Lvl. {HeroeActual.InfoCaracteristicas.Nivel}";
            string lineaTipos = $"Tipo(s): {String.Join(", ", HeroeActual.InfoDatos.Tipos)}";
            string lineaVida = $"Vida: {vidaCampeon}";
            WriteLine(lineaPokemon);
            WriteLine(lineaTipos);
            WriteLine(lineaVida);
            
            // Muestra los nombres del campeon y del jefe
            ForegroundColor = ConsoleColor.Red;
            lineaPokemon = $"{JefeActual.InfoDatos.Nombre} Lvl. {JefeActual.InfoCaracteristicas.Nivel}";
            lineaTipos = $"Tipo(s): {String.Join(", ", JefeActual.InfoDatos.Tipos)}";
            lineaVida = $"Vida: {vidaJefe}";

            CursorLeft = BufferWidth - lineaPokemon.Length - 3;
            WriteLine(lineaPokemon);
            CursorLeft = BufferWidth - lineaTipos.Length - 3;
            WriteLine(lineaTipos);
            CursorLeft = BufferWidth - lineaVida.Length - 3;
            WriteLine(lineaVida);
            WriteLine();

            ResetColor();
        }

        public void MostrarHeroes()
        {
            for (int i = 0; i < Heroes.Count; i++)
            {
                Clear();
                Interfaz.MostrarTextoCentrado($"\nTus Pokemon N° {i+1}:\n", ConsoleColor.Cyan);
                Heroes[i].MostrarPersonaje();
                ConsoleKey TeclaPresionada;
                WriteLine();
                if (i != Heroes.Count-1)
                {        
                    Interfaz.MostrarTextoCentrado("Presiona [Enter] para ver el siguiente pokemon.", ConsoleColor.DarkGray);
                    TeclaPresionada = ReadKey(true).Key;
                    while (TeclaPresionada != ConsoleKey.Enter) TeclaPresionada = ReadKey(true).Key;
                }
                else
                {
                    Interfaz.MostrarTextoCentrado("Presiona [Enter] para empezar la batalla.", ConsoleColor.DarkGray);
                    TeclaPresionada = ReadKey(true).Key;
                    while (TeclaPresionada != ConsoleKey.Enter) TeclaPresionada = ReadKey(true).Key;
                }
            }
        }

        public void Recompensas(string NombreJefeVencido, bool subeNivel = true)
        {
            // El heroe vencedor sube un nivel si es que no se encuentra en su maximo nivel
            if (HeroeActual.InfoCaracteristicas.Nivel < 10 && subeNivel) HeroeActual.InfoCaracteristicas.Nivel++;
            
            string Mensaje = "Felicidades!";
            Mensaje += $"\nVenciste a {NombreJefeVencido} y ganaste el combate!";
            Mensaje += $"\nPasaste a la Ronda {Ronda}.";
            Mensaje += "\n\nElige tu recompensa";

            List<string> OpcionesRecompensas = [$"Restaurar 10 de vida a {HeroeActual.InfoDatos.Nombre}", "Revivir un pokemon", $"Aumentar fuerza a {HeroeActual.InfoDatos.Nombre}", $"Aumentar defensa a {HeroeActual.InfoDatos.Nombre}"];

            Menu MenuRecompensas = new Menu(Mensaje, OpcionesRecompensas);
            int RecompensaElegida = MenuRecompensas.Ejecutar(ConsoleColor.Cyan);

            switch (RecompensaElegida)
            {
                case 0:
                    if (HeroeActual.InfoCaracteristicas.Salud < 91) HeroeActual.InfoCaracteristicas.Salud += 10;
                    else if (HeroeActual.InfoCaracteristicas.Salud < 101) HeroeActual.InfoCaracteristicas.Salud = 100;
                    else
                    {
                        // Si la vida esta en su maximo valor, llamo de vuelta la funcion para que escoja otra recompensa
                        WriteLine();
                        Interfaz.MostrarTextoCentrado("La vida ya esta en su maximo valor", ConsoleColor.Yellow);
                        Thread.Sleep(2000);
                        Recompensas(NombreJefeVencido, false);
                    }
                    break;
                case 1:
                    // Bandera para determinar si se revivio un pokemon o no
                    bool revivido = false;

                    foreach (Personaje pokemon in Heroes)
                    {
                        // Si el pokemon no tiene vida, le restauro la vida a 50
                        if (pokemon.InfoCaracteristicas.Salud < 0)
                        {
                            pokemon.InfoCaracteristicas.Salud = 50;
                            revivido = true;
                            break;
                        }
                    }

                    // Si no se revivio un pokemon, entonces todos los pokemones tienen vida
                    // y llamo la funcion para que elija otra recompensa
                    if (!revivido) 
                    {
                        WriteLine();
                        Interfaz.MostrarTextoCentrado("No tienes pokemones fuera de combate", ConsoleColor.Yellow);
                        Thread.Sleep(2000);
                        Recompensas(NombreJefeVencido, false);
                    }
                    break;
                case 2:
                    if (HeroeActual.InfoCaracteristicas.Fuerza < 10) HeroeActual.InfoCaracteristicas.Fuerza++;
                    else
                    {
                        // Si la fuerza esta en su maximo nivel, llamo de vuelta la funcion para que escoja otra recompensa
                        WriteLine();
                        Interfaz.MostrarTextoCentrado("La fuerza ya esta en su maximo nivel", ConsoleColor.Yellow);
                        Thread.Sleep(2000);
                        Recompensas(NombreJefeVencido, false);
                    }
                    break;
                case 3:
                    if (HeroeActual.InfoCaracteristicas.Defensa < 91) HeroeActual.InfoCaracteristicas.Defensa +=10;
                    else if (HeroeActual.InfoCaracteristicas.Defensa < 101) HeroeActual.InfoCaracteristicas.Defensa = 100;
                    else
                    {
                        // Si la defensa esta en su maximo nivel, llamo de vuelta la funcion para que escoja otra recompensa
                        WriteLine();
                        Interfaz.MostrarTextoCentrado("La defensa ya esta en su maximo nivel", ConsoleColor.Yellow);
                        Thread.Sleep(2000);
                        Recompensas(NombreJefeVencido, false);
                    }
                    break;
            }
        }

        public void SiguienteHeroe()
        {
            // De la lista de Heroes cambio al proximo campeon que pueda pelear (tenga vida mayor que 0)
            foreach (Personaje PosibleHeroe in Heroes)
            {
                if (PosibleHeroe.InfoCaracteristicas.Salud > 0)
                {
                    HeroeActual = PosibleHeroe;
                    break;
                }
            }
        }

        public void SiguienteJefe()
        {
            // De la lista de jefes cambio al proximo jefe que pueda pelear (tenga vida mayor que 0)
            foreach (Personaje posibleJefe in Jefes)
            {
                if (posibleJefe.InfoCaracteristicas.Salud > 0)
                {
                    JefeActual = posibleJefe;
                    break;
                }
            }
        }

        public float CalcularEfectividad(ElementoPokemon ElementoAtaque, List<ElementoPokemon> ElementoDefensa)
        {
            float Efectividad = 1;
            switch (ElementoAtaque)
            {
                case ElementoPokemon.Agua:
                    if (ElementoDefensa.Contains(ElementoPokemon.Fuego) || ElementoDefensa.Contains(ElementoPokemon.Tierra) || ElementoDefensa.Contains(ElementoPokemon.Roca)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Agua) || ElementoDefensa.Contains(ElementoPokemon.Planta) || ElementoDefensa.Contains(ElementoPokemon.Dragon)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Bicho:
                    if (ElementoDefensa.Contains(ElementoPokemon.Planta) || ElementoDefensa.Contains(ElementoPokemon.Psiquico) || ElementoDefensa.Contains(ElementoPokemon.Veneno)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Fuego) || ElementoDefensa.Contains(ElementoPokemon.Lucha) || ElementoDefensa.Contains(ElementoPokemon.Fantasma) || ElementoDefensa.Contains(ElementoPokemon.Volador)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Dragon:
                    if (ElementoDefensa.Contains(ElementoPokemon.Dragon)) Efectividad = 2;
                    break;
                case ElementoPokemon.Electrico:
                    if (ElementoDefensa.Contains(ElementoPokemon.Agua) || ElementoDefensa.Contains(ElementoPokemon.Volador)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Electrico) || ElementoDefensa.Contains(ElementoPokemon.Planta) || ElementoDefensa.Contains(ElementoPokemon.Dragon)) Efectividad = 0.5F;
                    if (ElementoDefensa.Contains(ElementoPokemon.Tierra)) Efectividad = 0;
                    break;
                case ElementoPokemon.Fantasma:
                    if (ElementoDefensa.Contains(ElementoPokemon.Fantasma)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Normal) || ElementoDefensa.Contains(ElementoPokemon.Psiquico)) Efectividad = 0;
                    break;
                case ElementoPokemon.Fuego:
                    if (ElementoDefensa.Contains(ElementoPokemon.Bicho) || ElementoDefensa.Contains(ElementoPokemon.Hielo) || ElementoDefensa.Contains(ElementoPokemon.Planta)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Agua) || ElementoDefensa.Contains(ElementoPokemon.Fuego) || ElementoDefensa.Contains(ElementoPokemon.Dragon) || ElementoDefensa.Contains(ElementoPokemon.Roca)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Hada:
                    if (ElementoDefensa.Contains(ElementoPokemon.Dragon)) Efectividad = 2;
                    break;
                case ElementoPokemon.Hielo:
                    if (ElementoDefensa.Contains(ElementoPokemon.Dragon) || ElementoDefensa.Contains(ElementoPokemon.Planta) || ElementoDefensa.Contains(ElementoPokemon.Tierra) || ElementoDefensa.Contains(ElementoPokemon.Volador)) Efectividad = 2;
                    else if (ElementoDefensa.Contains(ElementoPokemon.Agua) || ElementoDefensa.Contains(ElementoPokemon.Hielo)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Lucha:
                    if (ElementoDefensa.Contains(ElementoPokemon.Hielo) || ElementoDefensa.Contains(ElementoPokemon.Normal) || ElementoDefensa.Contains(ElementoPokemon.Roca)) Efectividad = 2;
                    else if (ElementoDefensa.Contains(ElementoPokemon.Bicho) || ElementoDefensa.Contains(ElementoPokemon.Psiquico) || ElementoDefensa.Contains(ElementoPokemon.Veneno) || ElementoDefensa.Contains(ElementoPokemon.Volador)) Efectividad = 0.5F;
                    if (ElementoDefensa.Contains(ElementoPokemon.Fantasma)) Efectividad = 0;
                    break;
                case ElementoPokemon.Normal:
                    if (ElementoDefensa.Contains(ElementoPokemon.Roca)) Efectividad = 0.5F;
                    if (ElementoDefensa.Contains(ElementoPokemon.Fantasma)) Efectividad = 0;
                    break;
                case ElementoPokemon.Planta:
                    if (ElementoDefensa.Contains(ElementoPokemon.Agua) || ElementoDefensa.Contains(ElementoPokemon.Tierra) || ElementoDefensa.Contains(ElementoPokemon.Roca)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Bicho) || ElementoDefensa.Contains(ElementoPokemon.Dragon) || ElementoDefensa.Contains(ElementoPokemon.Fuego) || ElementoDefensa.Contains(ElementoPokemon.Planta) || ElementoDefensa.Contains(ElementoPokemon.Veneno)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Psiquico:
                    if (ElementoDefensa.Contains(ElementoPokemon.Lucha) || ElementoDefensa.Contains(ElementoPokemon.Veneno)) Efectividad = 2;
                    else if (ElementoDefensa.Contains(ElementoPokemon.Psiquico)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Roca:
                    if (ElementoDefensa.Contains(ElementoPokemon.Bicho) || ElementoDefensa.Contains(ElementoPokemon.Fuego) || ElementoDefensa.Contains(ElementoPokemon.Hielo) || ElementoDefensa.Contains(ElementoPokemon.Volador)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Lucha) || ElementoDefensa.Contains(ElementoPokemon.Tierra)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Tierra:
                    if (ElementoDefensa.Contains(ElementoPokemon.Fuego) || ElementoDefensa.Contains(ElementoPokemon.Electrico) || ElementoDefensa.Contains(ElementoPokemon.Roca) || ElementoDefensa.Contains(ElementoPokemon.Veneno)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Bicho) || ElementoDefensa.Contains(ElementoPokemon.Planta)) Efectividad = 0.5F;
                    if (ElementoDefensa.Contains(ElementoPokemon.Volador)) Efectividad = 0;
                    break;
                case ElementoPokemon.Veneno:
                    if (ElementoDefensa.Contains(ElementoPokemon.Bicho) || ElementoDefensa.Contains(ElementoPokemon.Planta)) Efectividad = 2;
                    else if (ElementoDefensa.Contains(ElementoPokemon.Fantasma) || ElementoDefensa.Contains(ElementoPokemon.Roca) || ElementoDefensa.Contains(ElementoPokemon.Tierra) || ElementoDefensa.Contains(ElementoPokemon.Veneno)) Efectividad = 0.5F;
                    break;
                case ElementoPokemon.Volador:
                    if (ElementoDefensa.Contains(ElementoPokemon.Bicho) || ElementoDefensa.Contains(ElementoPokemon.Lucha) || ElementoDefensa.Contains(ElementoPokemon.Planta)) Efectividad = 2;
                    if (ElementoDefensa.Contains(ElementoPokemon.Electrico) || ElementoDefensa.Contains(ElementoPokemon.Roca)) Efectividad = 0.5F;
                    break;
            }
            return Efectividad;
        }
    }
}