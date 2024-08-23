using System.IO;
using System.Text.Json;
using static System.Console;

namespace Juego
{
    public static class FabricaDePersonajes
    {
        public static Personaje ObtenerPersonajeAleatorio()
        {
            // Trato de cargar la lista de personajes predeterminados llamando a la funcion LeerPersonajes() definida en la
            // clase estatica PersonajesJson y obtengo un personaje aleatorio (hago uso de un numero aleatorio como indice para lograrlo).
            // En caso que haya algún error en el intento retorno null.
            try
            {
                List<Personaje> PersonajesGuardados = PersonajesJson.LeerPersonajes();
                if (PersonajesGuardados.Count > 0)
                {
                    Random Aleatorio = new Random();
                    int RandomIndice = Aleatorio.Next(0, PersonajesGuardados.Count);
                    Personaje NuevoPersonaje = PersonajesGuardados[RandomIndice];
                    return NuevoPersonaje;
                }
                else return null;
            } 
            catch (JsonException error)
            {   
                ForegroundColor = ConsoleColor.DarkGray;
                ResetColor();
                Console.WriteLine($"Error al intentar procesar el archivo 'personajes.json': {error.Message}.");
                
                return null;
            }
            catch (Exception error)
            {
                ForegroundColor = ConsoleColor.DarkGray;
                ResetColor();
                Console.WriteLine($"Error al intentar acceder a los personajes predeterminados: {error.Message}.");
                
                return null;
            }
        }

        public static async Task<List<Personaje>> GenerarPersonajes(int CantPersonajes = 10)
        {
            // Creo una lista para insertar los nuevos personajes
            List<Personaje> NuevosPersonajes = new List<Personaje>();

            // Creo una lista que contendra los elementos del personaje creado
            List<ElementoPokemon> ListaTipos;

            // Creo un numero aleatorio que servira de id del pokemon. Toma valores del 1 al 151 (pokemones de la primera generacion)
            Random Aleatorio = new Random();
            int RandomId;

            // Variable que contendra el nombre del nuevo pokemon creado para insertar en la lista
            string NombrePoke;
            // Variable que contendra los datos del nuevo pokemon creado para insertar en la lista
            Datos NuevosDatos;
            // Variable que contendra las caracteristicas del nuevo pokemon creado para insertar en la lista
            Caracteristicas NuevasCaracteristicas;
            // Variable que contendra al nuevo pokemon creado para insertar en la lista
            Personaje NuevoPersonaje;

            for (int i = 0; i < CantPersonajes; i++)
            {
                // Si la respuesta de la api es valida, creo los datos y caracteristicas para instanciar un nuevo personaje
                // e insertarlo en la lista. En caso que haya cualquier error al intentarlo, obtengo un personaje llamando
                // a la funcion ObtenerPersonajeAleatorio() y lo agrego a la lista (en caso que sea una respuesta valida)
                Clear();
                Interfaz.MostrarTextoCentrado("\nGenerando Personajes...\nPor favor espere", ConsoleColor.Cyan);
                try
                {
                    RandomId = Aleatorio.Next(1,152);
                    var RandomPoke = await PokeApi.Pokemon(RandomId);
                    NombrePoke = Extra.Capitalizar(RandomPoke.name);
                
                    ListaTipos = new List<ElementoPokemon>();
                    foreach (typeInfo Tipo in RandomPoke.types)
                    {
                        ListaTipos.Add(TraducirTipo(Tipo.type.name));
                    }

                    NuevosDatos = new Datos(NombrePoke, ListaTipos);
                    NuevasCaracteristicas = new Caracteristicas();

                    NuevoPersonaje = new Personaje(NuevosDatos, NuevasCaracteristicas);
                    NuevosPersonajes.Add(NuevoPersonaje);

                    Interfaz.MostrarTextoCentrado("\nPersonajes creados exitosamente!", ConsoleColor.Cyan);
                }   
                catch (HttpRequestException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Error al intentar conectar con la pokeApi: {error.Message}.");
                    Console.WriteLine($"Obteniendo personaje predeterminado aleatorio...");
                    ResetColor();
                    
                    NuevoPersonaje = ObtenerPersonajeAleatorio();
                    if (NuevoPersonaje != null) NuevosPersonajes.Add(ObtenerPersonajeAleatorio());
                }
                catch (JsonException error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Error al intentar procesar la respuesta de la pokeApi: {error.Message}.");
                    Console.WriteLine($"Obteniendo personaje predeterminado aleatorio...");
                    ResetColor();

                    NuevoPersonaje = ObtenerPersonajeAleatorio();
                    if (NuevoPersonaje != null) NuevosPersonajes.Add(NuevoPersonaje);
                }
                catch (Exception error)
                {
                    ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Error al intentar generar un nuevo personaje: {error.Message}.");
                    Console.WriteLine($"Obteniendo personaje predeterminado aleatorio...");
                    ResetColor();

                    NuevoPersonaje = ObtenerPersonajeAleatorio();
                    if (NuevoPersonaje != null) NuevosPersonajes.Add(NuevoPersonaje);
                }
            }
            // En el peor escenario, donde fallan tanto la llamada a la api para crear el personaje como la funcion
            // ObtenerPersonajeAleatorio(), se devuelve una lista vacia.
            return NuevosPersonajes;
        }

        public static ElementoPokemon TraducirTipo(string tipo)
        {
            // Traduzco un string que contiene el nombre de un elemento pokemon en ingles y devuelvo el valor del tipo
            // ELementoPokemon correspondiente.
            ElementoPokemon ElePoke;
            switch (tipo)
            {
                case "steel":
                    ElePoke = ElementoPokemon.Acero;
                    break;
                case "water":
                    ElePoke = ElementoPokemon.Agua;
                    break;
                case "bug":
                    ElePoke = ElementoPokemon.Bicho;
                    break;
                case "dragon":
                    ElePoke = ElementoPokemon.Dragon;
                    break;
                case "electric":
                    ElePoke = ElementoPokemon.Electrico;
                    break;
                case "ghost":
                    ElePoke = ElementoPokemon.Fantasma;
                    break;
                case "fire":
                    ElePoke = ElementoPokemon.Fuego;
                    break;
                case "fairy":
                    ElePoke = ElementoPokemon.Hada;
                    break;
                case "ice":
                    ElePoke = ElementoPokemon.Hielo;
                    break;
                case "fighting":
                    ElePoke = ElementoPokemon.Lucha;
                    break;
                case "normal":
                    ElePoke = ElementoPokemon.Normal;
                    break;
                case "grass":
                    ElePoke = ElementoPokemon.Planta;
                    break;
                case "psychic":
                    ElePoke = ElementoPokemon.Psiquico;
                    break;
                case "rock":
                    ElePoke = ElementoPokemon.Roca;
                    break;
                case "ground":
                    ElePoke = ElementoPokemon.Tierra;
                    break;
                case "poison":
                    ElePoke = ElementoPokemon.Veneno;
                    break;
                case "flying":
                    ElePoke = ElementoPokemon.Volador;
                    break;
                default:
                    ElePoke = ElementoPokemon.Ninguno;
                    break;
            }
            return ElePoke;
        }

        public static ElementoPokemon ConvertirTipo(string tipo)
        {
            // Traduzco un string que contiene el nombre de un elemento pokemon en español y devuelvo el valor del tipo
            // ELementoPokemon correspondiente.
            ElementoPokemon ElePoke;
            switch (tipo)
            {
                case "Acero":
                    ElePoke = ElementoPokemon.Acero;
                    break;
                case "Agua":
                    ElePoke = ElementoPokemon.Agua;
                    break;
                case "Bicho":
                    ElePoke = ElementoPokemon.Bicho;
                    break;
                case "Dragon":
                    ElePoke = ElementoPokemon.Dragon;
                    break;
                case "Electrico":
                    ElePoke = ElementoPokemon.Electrico;
                    break;
                case "Fantasma":
                    ElePoke = ElementoPokemon.Fantasma;
                    break;
                case "Fuego":
                    ElePoke = ElementoPokemon.Fuego;
                    break;
                case "Hada":
                    ElePoke = ElementoPokemon.Hada;
                    break;
                case "Hielo":
                    ElePoke = ElementoPokemon.Hielo;
                    break;
                case "Lucha":
                    ElePoke = ElementoPokemon.Lucha;
                    break;
                case "Normal":
                    ElePoke = ElementoPokemon.Agua;
                    break;
                case "Planta":
                    ElePoke = ElementoPokemon.Planta;
                    break;
                case "Psiquico":
                    ElePoke = ElementoPokemon.Psiquico;
                    break;
                case "Roca":
                    ElePoke = ElementoPokemon.Roca;
                    break;
                case "Tierra":
                    ElePoke = ElementoPokemon.Tierra;
                    break;
                case "Veneno":
                    ElePoke = ElementoPokemon.Veneno;
                    break;
                case "Volador":
                    ElePoke = ElementoPokemon.Volador;
                    break;
                default:
                    ElePoke = ElementoPokemon.Ninguno;
                    break;
            }
            return ElePoke;
        }
    }
}