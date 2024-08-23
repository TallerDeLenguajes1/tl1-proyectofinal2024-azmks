using System.IO;
using System.Linq;

namespace Juego
{
    public static class Extra
    {

        // Funcion que chequea si existe un archivo en determinada ruta
        public static bool Existe(string rutaArchivo)
        {
            return rutaArchivo != null && File.Exists(rutaArchivo);
        }


        // Funcion que chequea si un archivo en determinada contiene informacion
        public static bool EstaVacio(string rutaArchivo)
        {
            if (Existe(rutaArchivo))
            {
                FileInfo miArchivo = new FileInfo(rutaArchivo);
                return miArchivo.Length == 0;
            }
            return false;
        }

        // Funcion que chequea si una carpeta en determinada ruta tiene elementos
        public static bool EsCarpetaVacia(string Ruta)
        {
            try
            {
                return !TieneElementos<string>(Directory.EnumerateFileSystemEntries(Ruta).ToList());
            }
            catch (IOException error)
            {
                Console.WriteLine($"Error al determinar si la carpeta {Ruta} se encuentra vacia: {error.Message}");
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error al determinar si la carpeta {Ruta} se encuentra vacia: {error.Message}");
                return true;
            }
        }

        // Funcion que chequea si lista de elementos del tipo T tiene elementos
        public static bool TieneElementos<T>(List<T> L) => L != null && L.Any();

        public static string Capitalizar(string s) => (s.Length > 1) ? string.Concat(s[0].ToString().ToUpper(), s.Substring(1)) : s;

        public static bool EsNombreValido(string palabra)
        {
            bool Resultado = true;
            
            // Si la palabra es null o es un string vacio entonces no es un nombre valido
            if (String.IsNullOrEmpty(palabra)) Resultado = false;
            // Si la palabra empieza con un digito entonces no es un nombre valido
            if (Char.IsDigit(palabra[0])) Resultado = false;
            
            // Verifico que para cada caracter de la palabra, esta sea una letra o digito, caso contrario
            // seteo el resultado en false y salgo del bucle pues no necesito seguir recorriendo la palabra
            for (int i = 0; i < palabra.Length; i++)
            {
                if (!Char.IsLetterOrDigit(palabra,i))
                {
                    Resultado = false;
                    break;
                }
            }

            

            return Resultado;
        }
    }
}