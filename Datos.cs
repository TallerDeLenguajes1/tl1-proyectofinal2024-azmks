using System.Text.Json.Serialization;

namespace Juego
{
    public class Datos
    {
        [JsonPropertyName("Nombre")]
        public string Nombre {get; set;}
        [JsonPropertyName("Genero")]
        public string Genero {get; set;}
        [JsonPropertyName("Edad")]
        public int Edad {get; set;}
        [JsonPropertyName("Tipos")]
        public List<ElementoPokemon> Tipos {get; set;}

        public Datos(string Nombre, List<ElementoPokemon> Tipos)
        {
            this.Nombre = Nombre;
            this.Tipos = Tipos;

            Random Aleatorio = new Random();

            if (Nombre == "Nidorina" || Nombre == "Nidoqueen") Genero = "Hembra";
            else if (Nombre == "Nidorino" || Nombre == "Nidoking") Genero = "Macho";
            else
            {
                int aux = Aleatorio.Next(0,2);
                if (aux == 0) Genero = "Hembra";
                else Genero = "Macho";
            }

            Edad = Aleatorio.Next(1,301);
        }
    }

    public enum ElementoPokemon
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
        Lucha,
        Normal,
        Planta,
        Psiquico,
        Roca,
        Tierra,
        Veneno,
        Volador,
        Ninguno
    }
}