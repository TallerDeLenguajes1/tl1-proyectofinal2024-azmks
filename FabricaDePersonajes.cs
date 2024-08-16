using System;
using System.IO;
using System.Text;
using System.Text.Json;
using static System.Console;
using System.Collections.Generic;

namespace Juego
{
    public class FabricaDePersonajes
    {
        private ApiConfig pokeApi = new ApiConfig();
        private Random Aleatorio = new Random();

        public ElementoPokemon convertirTipo(string tipo)
        {
            switch (tipo)
            {      
                case "Acero":
                    return ElementoPokemon.Acero;
                    break;
                case "Agua":
                    return ElementoPokemon.Agua;
                    break;
                case "Bicho":
                    return ElementoPokemon.Bicho;
                    break;
                case "Dragon":
                    return ElementoPokemon.Dragon;
                    break;
                case "Electrico":
                    return ElementoPokemon.Electrico;
                    break;
                case "Fantasma":
                    return ElementoPokemon.Fantasma;
                    break;
                case "Fuego":
                    return ElementoPokemon.Fuego;
                    break;
                case "Hada":
                    return ElementoPokemon.Hada;
                    break;
                case "Hielo":
                    return ElementoPokemon.Hielo;
                    break;
                case "Lucha":
                    return ElementoPokemon.Lucha;
                    break;
                case "Normal":
                    return ElementoPokemon.Normal;
                    break;
                case "Planta":
                    return ElementoPokemon.Planta;
                    break;
                case "Psiquico":
                    return ElementoPokemon.Psiquico;
                    break;
                case "Roca":
                    return ElementoPokemon.Roca;
                    break;
                case "Tierra":
                    return ElementoPokemon.Tierra;
                    break;
                case "Veneno":
                    return ElementoPokemon.Veneno;
                    break;
                case "Volador":
                    return ElementoPokemon.Volador;
                    break;
            }
        }
        public ElementoPokemon traducirTipos(string tipo)
        {
            switch (tipo)
            {      
                case "steel":
                    return ElementoPokemon.Acero;
                    break;
                case "water":
                    return ElementoPokemon.Agua;
                    break;
                case "bug":
                    return ElementoPokemon.Bicho;
                    break;
                case "dragon":
                    return ElementoPokemon.Dragon;
                    break;
                case "electric":
                    return ElementoPokemon.Electrico;
                    break;
                case "ghost":
                    return ElementoPokemon.Fantasma;
                    break;
                case "fire":
                    return ElementoPokemon.Fuego;
                    break;
                case "fairy":
                    return ElementoPokemon.Hada;
                    break;
                case "ice":
                    return ElementoPokemon.Hielo;
                    break;
                case "fighting":
                    return ElementoPokemon.Lucha;
                    break;
                case "normal":
                    return ElementoPokemon.Normal;
                    break;
                case "grass":
                    return ElementoPokemon.Planta;
                    break;
                case "psychic":
                    return ElementoPokemon.Psiquico;
                    break;
                case "rock":
                    return ElementoPokemon.Roca;
                    break;
                case "ground":
                    return ElementoPokemon.Tierra;
                    break;
                case "poison":
                    return ElementoPokemon.Veneno;
                    break;
                case "flying":
                    return ElementoPokemon.Volador;
                    break;
            }
        }

        // Metodo para crear pokemones con informacion obtenida de la pokeApi
        public async Task<Personaje> crearPersonaje()
        {
            PersonajeJson nuevoPoke = await pokeApi.PokemonRandom();
            if (nuevoPoke == null)
            {
                Interfaz.mostrarTextoCentrado("Intento fallido de obtener un pokemon aleatorio de la pokeApi", ConsoleColor.White);
                return null;
            }
            else
            {
                string nuevoNombre = nuevoPoke.name;
                List<ElementoPokemon> nuevoPokeTipos = new List<ElementoPokemon>();

                foreach(typeInfo infoElemento in nuevoPoke.types)
                {
                    nuevoPokeTipos.Add(traducirTipos(infoElemento.type.name));
                }

                Datos nuevosDatos = new Datos(nuevoNombre, nuevoPokeTipos);
                Caracteristicas nuevasCaracteristicas = new Caracteristicas();
                crearPersonaje nuevoPersonaje = new crearPersonaje(nuevosDatos, nuevasCaracteristicas);
                return nuevoPersonaje;
                conexion = true;
            }
        }

        // Metodo para obtener una lista de pokemones precargados
        public List<Personaje> obtenerPersonajes(int cantidad)
        {
            try
            {
                // Creo una lista de personajes vacia
                List<Personaje> nuevosPersonajes = new List<Personaje>();

                // Cargo en una lista los 25 pokemones precargados utilizando un archivo json
                string predeterminadosJson = File.ReadAllText("personajes.json");
                List<PredeterminadoJson> pokeLista = JsonSerializer.Deserialize<List<PredeterminadoJson>>(predeterminadosJson);
                int index;

                // Genero numeros aleatorios para poder acceder a pokemones aleatorios en la lista de precargados
                for (int i = 0; i < cantidad; i++)
                {
                    index = Aleatorio.Next(0,26);

                    // Creo los nombres, tipos, datos y caracteristicas de los personajes para añadirlos en la lista
                    string nombre = pokeLista[index].Nombre;
                    List<ElementoPokemon> tipos = new List<ElementoPokemon>();

                    // De la lista de tipos (que contiene elementos del tipo string) que obtuve del json, llamo a una funcion para
                    // convertirlos en el tipo ELementoTipo para poder agregarlos a la lista Tipos (que es del tipo ElementosPokemon)
                    foreach (string tipoStr in pokeLista[index].Tipos)
                    {
                        tipos.Add(convertirTipo(tipoStr));
                    }

                    Datos nuevosDatos = new Datos(nombre, tipos);
                    Caracteristicas nuevasCaracteristicas = new Caracteristicas();

                    nuevosPersonajes.Add(new Personaje(nuevosDatos, nuevasCaracteristicas));
                }
                
                return nuevosPersonajes;
            }
            catch (JsonException error)
            {
                Interfaz.mostrarTextoCentrado($"Error inesperado al intentar cargar un personaje predeterminado. Error: {error.Message}", ConsoleColor.White);
            }
            catch (Exception error)
            {
                Interfaz.mostrarTextoCentrado($"Error inesperado al intentar cargar un personaje predeterminado. Error: {error.Message}", ConsoleColor.White);
            }
            return null;
        }

        // Metodo para generar los pokemones iniciales
        public async Task<List<Personaje>> generarPersonajesIniciales()
        {
            Clear();
            int cantPersonajes = 10;

            // Creo un menu para que el usuario decida si quiere utilizar la api o utilizar valores guardados
            Menu preguntarPorApi = new Menu("¿Como desea obtener sus pokemones?", ["Utilizar pokeApi", "Utilizar Pokemones Predeterminados"]);
            int eleccion = preguntarPorApi.Ejecutar(ConsoleColor.Cyan);

            switch (eleccion)
            {
                case 0:
                    int intento = 1;
                    bool conexionEstablecida = false;

                    // Intento conectar con la api hasta 5 veces, si no se logro generar una conexion
                    // se obtendra los pokemones por predeterminado
                    while (intento < 6 && !conexionEstablecida)
                    {
                        Interfaz.mostrarTextoCentrado("Intentando conectar con la pokeApi...", ConsoleColor.White);
                        try
                        {
                            List<Personaje> nuevosPersonajes = new List<Personaje>();
                            for (int i = 0; i < cantPersonajes; i++) nuevosPersonajes.Add(await crearPersonaje());
                            conexionEstablecida = true;
                        }
                        catch (HttpRequestException)
                        {
                            intento++;
                        }
                    }

                    if (!conexionEstablecida)
                    {
                        List<Personaje> nuevosPersonajes = obtenerPersonajes(cantPersonajes);
                    }
                    break;
                case 1:
                    List<Personaje> nuevosPersonajes = obtenerPersonajes(cantPersonajes);
                    break;
            }
            return nuevosPersonajes;
        }
    }

    public class PredeterminadoJson
    {
        [JsonPropertyName("Nombre")]
        public string Nombre {get; set;}
        [JsonPropertyName("Tipos")]
        public List<string> Tipos {get; set;}
    }
}