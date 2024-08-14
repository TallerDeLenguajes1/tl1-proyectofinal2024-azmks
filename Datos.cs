namespace Juego
{
    public class Datos
    {
        private TipoPokemon Tipo;
        private TipoPokemon[] Debilidades;
        private string Nombre;
        private string Apodo;
        private int Edad;
        private PokeGenero Genero ;
    }

    public enum TipoPokemon
    {
        Acero,
        Agua,
        Bicho,
        Dragon,
        Electrico,
        Fantasma,
        Fuego,
        Hada,
        Hielo,
        Luca,
        Normal,
        Planta,
        Psiquico,
        Roca,
        Tierra,
        Veneno,
        Volador
    }

    public enum PokeGenero
    {
        Hembra,
        Macho
    }
}