using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using static System.Console;

namespace Juego
{
    public class PartidaJson
    {
        [JsonPropertyName("nombre")]
        private string Nombre;
        [JsonPropertyName("ronda")]
        private int RondaActual;
        [JsonPropertyName("personajes")]
        private Pokemon[] personajes;
        [JsonPropertyName("jefes")]
        private Pokemon[] Jefes;

        public void obtenerNombre()
        {
            Clear();
            string instruccion = "¿Como te llamas?\nTu nombre debe tener una longitud entre 3 y 10 caracteres (letras o digitos). Ademas, debe comenzar con una letra.";
            string nombre = String.Empty;

            do
            {
                Clear();
                Interfaz.mostrarTextoCentrado(instruccion, ConsoleColor.Cyan);
                SetCursorPosition((WindowWidth-10)/2, Console.CursorTop);
                nombre = ReadLine();
            }
            while (nombre.Length < 3 || nombre.Length > 10 || !Interfaz.esEntradaValida(nombre));

            Clear();
            ResetColor();
            this.Nombre = nombre;
        }
        /*
        public void guardarPartida()
        {

        }

        public void guardarPokemones()
        {

        }

        public void guardarJefes()
        {

        }
        */
    }
}