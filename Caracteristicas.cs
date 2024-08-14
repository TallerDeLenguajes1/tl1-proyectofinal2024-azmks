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
            Velocidad = Aleatorio.Next(1, 11);
            Destreza = Aleatorio.Next(1, 11);
            Fuerza = Aleatorio.Next(1, 11);
            Nivel = Aleatorio.Next(1, 11);
            Salud = 100;
        }
    }

}