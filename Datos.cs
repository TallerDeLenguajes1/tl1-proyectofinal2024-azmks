using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Juego
{
    public class Datos
    {
        [JsonPropertyName("Nombre")]
        public string Nombre {get; set;}
        
        [JsonPropertyName("Tipos")]
        public List<ElementoPokemon> Tipos {get; set;}
        
        [JsonPropertyName("Debilidades")]
        public List<ElementoPokemon> Debilidades {get; set;}
        
        [JsonPropertyName("Edad")]
        public int Edad {get; set;}

        public Datos(string name, List<ElementoPokemon> Types)
        {
            Nombre = name;
            Tipos = new List<ElementoPokemon>();
            Debilidades = new List<ElementoPokemon>();
            foreach (ElementoPokemon Tipo in Types)
            {
                Tipos.Add(Tipo);
                determinarDebilidades(Tipo);
            }
            Radom rnd = new Random();
            Edad = rnd.Next(0,301);
        }

        public void determinarDebilidades(ElementoPokemon Tipo)
        {
            // Segun el elemento pokemon recibido agrega a la lista de debilidades el elemento correspondiente si es que no se encuentra en la lista
            switch (ElementoPokemon)
            {
                case ElementoPokemon.Acero:
                    if (!Debilidades.Contains(ElementoPokemon.Fuego)) Debilidades.Add(ElementoPokemon.Fuego);
                    if (!Debilidades.Contains(ElementoPokemon.Lucha)) Debilidades.Add(ElementoPokemon.Lucha);
                    if (!Debilidades.Contains(ElementoPokemon.Tierra)) Debilidades.Add(ElementoPokemon.Tierra);
                    break;
                case ElementoPokemon.Agua:
                    if (!Debilidades.Contains(ElementoPokemon.Electrico)) Debilidades.Add(ElementoPokemon.Electrico);
                    if (!Debilidades.Contains(ElementoPokemon.Planta)) Debilidades.Add(ElementoPokemon.Planta);
                    break;
                case ElementoPokemon.Bicho:
                    if (!Debilidades.Contains(ElementoPokemon.Fuego)) Debilidades.Add(ElementoPokemon.Fuego);
                    if (!Debilidades.Contains(ElementoPokemon.Volador)) Debilidades.Add(ElementoPokemon.Volador);
                    if (!Debilidades.Contains(ElementoPokemon.Roca)) Debilidades.Add(ElementoPokemon.Roca);
                    break;
                case ElementoPokemon.Dragon:
                    if (!Debilidades.Contains(ElementoPokemon.Hada)) Debilidades.Add(ElementoPokemon.Hada);
                    if (!Debilidades.Contains(ElementoPokemon.Hielo)) Debilidades.Add(ElementoPokemon.Hielo);
                    if (!Debilidades.Contains(ElementoPokemon.Dragon)) Debilidades.Add(ElementoPokemon.Dragon);
                    break;
                case ElementoPokemon.Electrico:
                    if (!Debilidades.Contains(ElementoPokemon.Tierra)) Debilidades.Add(ElementoPokemon.Tierra);
                    break;
                case ElementoPokemon.Fantasma:
                    if (!Debilidades.Contains(ElementoPokemon.Fantasma)) Debilidades.Add(ElementoPokemon.Fantasma);
                    break;
                case ElementoPokemon.Fuego:
                    if (!Debilidades.Contains(ElementoPokemon.Agua)) Debilidades.Add(ElementoPokemon.Agua);
                    if (!Debilidades.Contains(ElementoPokemon.Roca)) Debilidades.Add(ElementoPokemon.Roca);
                    if (!Debilidades.Contains(ElementoPokemon.Tierra)) Debilidades.Add(ElementoPokemon.Tierra);
                    break;
                case ElementoPokemon.Hada:
                    if (!Debilidades.Contains(ElementoPokemon.Acero)) Debilidades.Add(ElementoPokemon.Acero);
                    break;
                case ElementoPokemon.Hielo:
                    if (!Debilidades.Contains(ElementoPokemon.Acero)) Debilidades.Add(ElementoPokemon.Acero);
                    if (!Debilidades.Contains(ElementoPokemon.Fuego)) Debilidades.Add(ElementoPokemon.Fuego);
                    if (!Debilidades.Contains(ElementoPokemon.Lucha)) Debilidades.Add(ElementoPokemon.Lucha);
                    if (!Debilidades.Contains(ElementoPokemon.Roca)) Debilidades.Add(ElementoPokemon.Roca);
                    break;
                case ElementoPokemon.Lucha:
                    if (!Debilidades.Contains(ElementoPokemon.Hada)) Debilidades.Add(ElementoPokemon.Hada);
                    if (!Debilidades.Contains(ElementoPokemon.Psiquico)) Debilidades.Add(ElementoPokemon.Psiquico);
                    if (!Debilidades.Contains(ElementoPokemon.Volador)) Debilidades.Add(ElementoPokemon.Volador);
                    break;
                case ElementoPokemon.Normal:
                    if (!Debilidades.Contains(ElementoPokemon.Lucha)) Debilidades.Add(ElementoPokemon.Lucha);
                    break;
                case ElementoPokemon.Planta:
                    if (!Debilidades.Contains(ElementoPokemon.Bicho)) Debilidades.Add(ElementoPokemon.Bicho);
                    if (!Debilidades.Contains(ElementoPokemon.Fuego)) Debilidades.Add(ElementoPokemon.Fuego);
                    if (!Debilidades.Contains(ElementoPokemon.Hielo)) Debilidades.Add(ElementoPokemon.Hielo);
                    if (!Debilidades.Contains(ElementoPokemon.Veneno)) Debilidades.Add(ElementoPokemon.Veneno);
                    if (!Debilidades.Contains(ElementoPokemon.Volador)) Debilidades.Add(ElementoPokemon.Volador);
                    break;
                case ElementoPokemon.Psiquico:
                    if (!Debilidades.Contains(ElementoPokemon.Bicho)) Debilidades.Add(ElementoPokemon.Bicho);
                    if (!Debilidades.Contains(ElementoPokemon.Fantasma)) Debilidades.Add(ElementoPokemon.Fantasma);
                    break;
                case ElementoPokemon.Roca:
                    if (!Debilidades.Contains(ElementoPokemon.Acero)) Debilidades.Add(ElementoPokemon.Acero);
                    if (!Debilidades.Contains(ElementoPokemon.Agua)) Debilidades.Add(ElementoPokemon.Agua);
                    if (!Debilidades.Contains(ElementoPokemon.Lucha)) Debilidades.Add(ElementoPokemon.Lucha);
                    if (!Debilidades.Contains(ElementoPokemon.Planta)) Debilidades.Add(ElementoPokemon.Planta);
                    if (!Debilidades.Contains(ElementoPokemon.Tierra)) Debilidades.Add(ElementoPokemon.Tierra);
                    break;
                case ElementoPokemon.Tierra:
                    if (!Debilidades.Contains(ElementoPokemon.Agua)) Debilidades.Add(ElementoPokemon.Agua);
                    if (!Debilidades.Contains(ElementoPokemon.Planta)) Debilidades.Add(ElementoPokemon.Planta);
                    break;
                case ElementoPokemon.Veneno:
                    if (!Debilidades.Contains(ElementoPokemon.Psiquico)) Debilidades.Add(ElementoPokemon.Psiquico);
                    if (!Debilidades.Contains(ElementoPokemon.Tierra)) Debilidades.Add(ElementoPokemon.Tierra);
                    break;
                case ElementoPokemon.Volador:
                    if (!Debilidades.Contains(ElementoPokemon.Electrico)) Debilidades.Add(ElementoPokemon.Electrico);
                    if (!Debilidades.Contains(ElementoPokemon.Hielo)) Debilidades.Add(ElementoPokemon.Hielo);
                    if (!Debilidades.Contains(ElementoPokemon.Roca)) Debilidades.Add(ElementoPokemon.Roca);
                    break;
            }
        }
    }
}