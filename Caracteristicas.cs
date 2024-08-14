namespace Juego
{
    public class Caracteristicas
    {
        private int Velocidad;
        private int Fuerza;
        private int Nivel;
        private int Salud;
        private Random Aleatorio = new Random();
        public Caracteristicas()
        {
            Velocidad = Aleatorio.Next(1, 10);
            Destreza = Aleatorio.Next(1, 10);
            Fuerza = Aleatorio.Next(1, 10);
            Nivel = Aleatorio.Next(1, 10);
            Salud = 100;
        }
    }

}