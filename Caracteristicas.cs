using System.Text.Json.Serialization;

namespace Juego
{
    public class Caracteristicas
    {
        [JsonPropertyName("Salud")]
        public int Salud {get; set;}
        [JsonPropertyName("Fuerza")]
        public int Fuerza {get; set;}
        [JsonPropertyName("Defensa")]
        public int Defensa {get; set;}
        [JsonPropertyName("Nivel")]
        public int Nivel {get; set;}
        
        public Caracteristicas()
        {
            Random Aleatorio = new Random();
            Salud = 100;
            Fuerza = Aleatorio.Next(1,11);
            Defensa = Aleatorio.Next(1,101);
            Nivel = Aleatorio.Next(1,11);
        }
    }
}