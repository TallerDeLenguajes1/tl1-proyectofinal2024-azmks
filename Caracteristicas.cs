using System.Text.Json.Serialization;

namespace Juego
{
    public class Caracteristicas
    {
        public int Salud {get; set;}
        public int Fuerza {get; set;}
        public int Defensa {get; set;}
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