using System.Text.Json;

namespace EjercicioCine.Classes
{
    public class Director
    {
        public int DirectorId { get; set; }
        public string? DirectorName { get; set; }


        public static List<Director> LoadDirector()
        {

            var directors = new List<Director>();
            try
            {
                var sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\directors.txt");
                var sFilePath = Path.GetFullPath(sFile);
                var json = File.ReadAllText(sFilePath);
                directors = JsonSerializer.Deserialize<List<Director>>(json);
            }
            catch (Exception)
            {
                Console.WriteLine("El archivo directors.txt no se pudo abrir.");
            }

            return directors;

        }
    }
}
