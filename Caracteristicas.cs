using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Juego
{
    public class Caracteristicas
    {
        [JsonPropertyName("Fuerza")]
        public int Fuerza {get; set;}
        [JsonPropertyName("Nivel")]
        public int Nivel {get; set;}
        [JsonPropertyName("Salud")]
        public int Salud {get; set;}
        [JsonPropertyName("Defensa")]
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