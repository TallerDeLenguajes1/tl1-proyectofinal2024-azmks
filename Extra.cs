namespace Juego
{
    public static class Extra(string ruta)
    {
        public static bool ExisteArchivo()
        {
            if (!File.Exists(ruta))
            {
                return false;
            }

            var info = new FileInfo(ruta);
            if (info.Length == 0)
            {
                return false
            }
            else
            {
                return true;
            }
        }
    }
}