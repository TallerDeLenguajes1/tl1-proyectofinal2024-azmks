namespace Juego
{
    public class Partida
    {
        public string Nombre {get; set;}
        public int Ronda {get; set;}
        public List<Personaje> Heroes {get; set;}
        public List<Personaje> Jefes {get; set;}

        private Personaje HeroeActual;
        private Personaje JefeActual;

        public Partida(string Nombre, List<Personaje> Heroes, List<Personaje> Jefes, int Ronda )
        {
            this.Nombre = Nombre;
            this.Heroes = Heroes;
            this.Jefes = Jefes;
            this.Ronda = Ronda;

            // Si las listas recibidas tienen por lo menos un elemento, asigno el primer elemento al heroe actual y al jefe
            if (Extra.TieneElementos<Personaje>(Heroes)) HeroeActual = Heroes[0];
            if (Extra.TieneElementos<Personaje>(Jefes)) JefeActual = Jefes[0];
        }
        /*
        public void Combate()
        {

        }

        public int calcularDano(int fuerza, int nivel, int defensa, int ElementoAtaque, List<ElementoPokemon> ElementoDefensa)
        {
            // Le asigno un numero aleatorio entre 1 y 100 a la precision
            int Precision = Aleatorio.Next(1, 101);
            int dano = ((fuerza * nivel * Precision) - defensa) * Efectividad / 500;
            int Efectividad = CalcularEfectividad(int ElementoAtaque, List<Elementos)
            
            // Devuelvo el maximo entre el daño generado y 0, así en caso que el daño sea negativo se devolvera 0
            return Math.Max(0, dano);
        }

        public void cambiarCampeon()
        {
            // Creo una lista de los nombres de los Heroes y le inserto aquellos que pueden pelear (tienen una vida mayor que 0)
            List<string> HeroesDisponibles = new List<Personaje>();
            foreach (Personaje posibleCampeon in HeroesLista)
            {
                if (posibleCampeon.InfoCaracteristicas.Salud > 0)
                {
                    HeroesDisponibles.Add(posibleCampeon.InfoDatos.Nombre);
                }
            }

            // Ejecuto el menu y cambio el campeon al seleccionado
            Menu menuHeroes = new Menu("Seleccione el pokemon que desea utilizar en batalla.", HeroesDisponibles);
            int eleccion = menuHeroes.Ejecutar(ConsoleColor.Green);
            Campeon = HeroesDisponibles[eleccion];
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
            Interfaz.mostrarTextoCentrado($"{Jefe.InfoDatos.Nombre} utilizo "+ataque, ConsoleColor.Red);

            // Espera 3 segundos antes de continuar
            System.Threading.Thread.Sleep(3000);
        }
        public void siguienteCampeon()
        {
            // De la lista de Heroes cambio al proximo campeon que pueda pelear (tenga vida mayor que 0)
            foreach (Personaje posibleCampeon in HeroesLista)
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
        
        private void DeterminarDebilidades()
        {
            foreach(ElementoPokemon Tipo in Tipos)
            {
                // Segun el elemento pokemon recibido agrega a la lista de debilidades el elemento correspondiente si es que no se encuentra en la lista
                switch (Tipo)
                {
                    case ElementoPokemon.Acero:
                        if (ElementoPokemon.Fuego) Debilidades.Add(ElementoPokemon.Fuego);
                        if (ElementoPokemon.Lucha) Debilidades.Add(ElementoPokemon.Lucha);
                        if (ElementoPokemon.Tierra) Debilidades.Add(ElementoPokemon.Tierra);
                        break;
                    case ElementoPokemon.Agua:
                        if (ElementoPokemon.Electrico) Debilidades.Add(ElementoPokemon.Electrico);
                        if (ElementoPokemon.Planta) Debilidades.Add(ElementoPokemon.Planta);
                        break;
                    case ElementoPokemon.Bicho:
                        if (ElementoPokemon.Fuego) Debilidades.Add(ElementoPokemon.Fuego);
                        if (ElementoPokemon.Volador) Debilidades.Add(ElementoPokemon.Volador);
                        if (ElementoPokemon.Roca) Debilidades.Add(ElementoPokemon.Roca);
                        break;
                    case ElementoPokemon.Dragon:
                        if (ElementoPokemon.Hada) Debilidades.Add(ElementoPokemon.Hada);
                        if (ElementoPokemon.Hielo) Debilidades.Add(ElementoPokemon.Hielo);
                        if (ElementoPokemon.Dragon) Debilidades.Add(ElementoPokemon.Dragon);
                        break;
                    case ElementoPokemon.Electrico:
                        if (ElementoPokemon.Tierra) Debilidades.Add(ElementoPokemon.Tierra);
                        break;
                    case ElementoPokemon.Fantasma:
                        if (ElementoPokemon.Fantasma) Debilidades.Add(ElementoPokemon.Fantasma);
                        break;
                    case ElementoPokemon.Fuego:
                        if (ElementoPokemon.Agua) Debilidades.Add(ElementoPokemon.Agua);
                        if (ElementoPokemon.Roca) Debilidades.Add(ElementoPokemon.Roca);
                        if (ElementoPokemon.Tierra) Debilidades.Add(ElementoPokemon.Tierra);
                        break;
                    case ElementoPokemon.Hada:
                        if (ElementoPokemon.Acero) Debilidades.Add(ElementoPokemon.Acero);
                        break;
                    case ElementoPokemon.Hielo:
                        if (ElementoPokemon.Acero) Debilidades.Add(ElementoPokemon.Acero);
                        if (ElementoPokemon.Fuego) Debilidades.Add(ElementoPokemon.Fuego);
                        if (ElementoPokemon.Lucha) Debilidades.Add(ElementoPokemon.Lucha);
                        if (ElementoPokemon.Roca) Debilidades.Add(ElementoPokemon.Roca);
                        break;
                    case ElementoPokemon.Lucha:
                        if (ElementoPokemon.Hada) Debilidades.Add(ElementoPokemon.Hada);
                        if (ElementoPokemon.Psiquico) Debilidades.Add(ElementoPokemon.Psiquico);
                        if (ElementoPokemon.Volador) Debilidades.Add(ElementoPokemon.Volador);
                        break;
                    case ElementoPokemon.Normal:
                        if (ElementoPokemon.Lucha) Debilidades.Add(ElementoPokemon.Lucha);
                        break;
                    case ElementoPokemon.Planta:
                        if (ElementoPokemon.Bicho) Debilidades.Add(ElementoPokemon.Bicho);
                        if (ElementoPokemon.Fuego) Debilidades.Add(ElementoPokemon.Fuego);
                        if (ElementoPokemon.Hielo) Debilidades.Add(ElementoPokemon.Hielo);
                        if (ElementoPokemon.Veneno) Debilidades.Add(ElementoPokemon.Veneno);
                        if (ElementoPokemon.Volador) Debilidades.Add(ElementoPokemon.Volador);
                        break;
                    case ElementoPokemon.Psiquico:
                        if (ElementoPokemon.Bicho) Debilidades.Add(ElementoPokemon.Bicho);
                        if (ElementoPokemon.Fantasma) Debilidades.Add(ElementoPokemon.Fantasma);
                        break;
                    case ElementoPokemon.Roca:
                        if (ElementoPokemon.Acero) Debilidades.Add(ElementoPokemon.Acero);
                        if (ElementoPokemon.Agua) Debilidades.Add(ElementoPokemon.Agua);
                        if (ElementoPokemon.Lucha) Debilidades.Add(ElementoPokemon.Lucha);
                        if (ElementoPokemon.Planta) Debilidades.Add(ElementoPokemon.Planta);
                        if (ElementoPokemon.Tierra) Debilidades.Add(ElementoPokemon.Tierra);
                        break;
                    case ElementoPokemon.Tierra:
                        if (ElementoPokemon.Agua) Debilidades.Add(ElementoPokemon.Agua);
                        if (ElementoPokemon.Planta) Debilidades.Add(ElementoPokemon.Planta);
                        break;
                    case ElementoPokemon.Veneno:
                        if (ElementoPokemon.Psiquico) Debilidades.Add(ElementoPokemon.Psiquico);
                        if (ElementoPokemon.Tierra) Debilidades.Add(ElementoPokemon.Tierra);
                        break;
                    case ElementoPokemon.Volador:
                        if (ElementoPokemon.Electrico) Debilidades.Add(ElementoPokemon.Electrico);
                        if (ElementoPokemon.Hielo) Debilidades.Add(ElementoPokemon.Hielo);
                        if (ElementoPokemon.Roca) Debilidades.Add(ElementoPokemon.Roca);
                        break;
                }
            }
        }*/
    }
}