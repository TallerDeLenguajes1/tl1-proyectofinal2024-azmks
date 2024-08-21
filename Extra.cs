using System.IO;

namespace Juego
{
    public static class Extra
    {
        public static bool Existe(string rutaArchivo)
        {
            return rutaArchivo != null && File.Exists(rutaArchivo);
        }

        public static bool EstaVacio(string rutaArchivo)
        {
            if (Existe(rutaArchivo))
            {
                FileInfo miArchivo = new FileInfo(rutaArchivo);
                return miArchivo.Length == 0;
            }
            return false;
        }

        public static bool TieneElementos<T>(List<T> L) => L != null && L.Any();

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