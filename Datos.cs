using System.Collections.Generic;

namespace Juego
{
    public class Datos
    {
        private string Nombre;
        private TipoPokemon Tipo1;
        private TipoPokemon Tipo2;
        private int Edad;
        private List<TipoPokemon> Debilidades;

        public Datos(string N, TipoPokemon T1, TipoPokemon T2)
        {
            Nombre = N;
            Tipo1 = T1;
            Debilidades = new List<TipoPokemon>();
            determinarDebilidades(Tipo1);
            if (IsNullOrEmpty(T2)) Tipo2 = string.Empty();
            else {
                Tipo2 = T2;
                determinarDebilidades(Tipo2);
            }

        }

        public determinarDebilidades(TipoPokemon Elemento)
        {
            switch (Elemento)
            {
                Acero:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Agua);
                    Debilidades.Add(Elemento.Electrico);
                    Debilidades.Add(Elemento.Fuego);
                    break;
                Agua:
                    Debilidades.Add(Elemento.Agua);
                    Debilidades.Add(Elemento.Dragon);
                    Debilidades.Add(Elemento.Planta);
                    break;
                Bicho:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Fantasma);
                    Debilidades.Add(Elemento.Fuego);
                    Debilidades.Add(Elemento.Hada);
                    Debilidades.Add(Elemento.Lucha);
                    Debilidades.Add(Elemento.Volador);
                    Debilidades.Add(Elemento.Veneno);
                    break;
                Dragon:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Hada);
                    break;
                Electrico:
                    Debilidades.Add(Elemento.Dragon);
                    Debilidades.Add(Elemento.Electrico);
                    Debilidades.Add(Elemento.Planta);
                    Debilidades.Add(Elemento.Tierra);
                    break;
                Fantasma:
                    break;
                Fuego:
                    Debilidades.Add(Elemento.Hada);
                    Debilidades.Add(Elemento.Dragon);
                    Debilidades.Add(Elemento.Fuego);
                    Debilidades.Add(Elemento.Roca);
                    break;
                Hada:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Fuego);
                    Debilidades.Add(Elemento.Veneno);
                    break;
                Hielo:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Agua);
                    Debilidades.Add(Elemento.Fuego);
                    Debilidades.Add(Elemento.Hielo);
                    break;
                Lucha:
                    Debilidades.Add(Elemento.Bicho);
                    Debilidades.Add(Elemento.Fantasma);
                    Debilidades.Add(Elemento.Hada);
                    Debilidades.Add(Elemento.Psiquico);
                    Debilidades.Add(Elemento.Veneno);,
                    Debilidades.Add(Elemento.Volador);
                    break;
                Normal:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Fantasma);
                    Debilidades.Add(Elemento.Roca);
                    break;
                Planta:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Bicho);
                    Debilidades.Add(Elemento.Dragon);
                    Debilidades.Add(Elemento.Fuego);
                    Debilidades.Add(Elemento.Planta);
                    Debilidades.Add(Elemento.Veneno);
                    Debilidades.Add(Elemento.Volador);
                    break;
                Psiquico:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Psiquico);
                    break;
                Roca:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Lucha);
                    Debilidades.Add(Elemento.Tierra);
                    break;
                Tierra:
                    Debilidades.Add(Elemento.Bicho);
                    Debilidades.Add(Elemento.Planta);
                    Debilidades.Add(Elemento.Volador);
                    break;
                Veneno:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Fantasma);
                    Debilidades.Add(Elemento.Roca);
                    Debilidades.Add(Elemento.Tierra);
                    Debilidades.Add(Elemento.Veneno);
                    break;
                Volador:
                    Debilidades.Add(Elemento.Acero);
                    Debilidades.Add(Elemento.Electrico);
                    Debilidades.Add(Elemento.Roca);
                    break;
            }
        }
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
}