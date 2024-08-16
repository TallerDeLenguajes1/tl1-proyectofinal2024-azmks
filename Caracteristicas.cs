using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Juego
{
    public class Caracteristicas
    {
        public int Fuerza {get; set;}
        public int Nivel {get; set;}
        public int Salud {get; set;}
        public int Defensa {get; set;}
        private Random Aleatorio = new Random();
        public Caracteristicas()
        {
            Fuerza = Aleatorio.Next(1, 11);
            Nivel = Aleatorio.Next(1, 11);
            Defensa = Aleatorio.Next(1, 101);
            Salud = 100;
        }
    }

}