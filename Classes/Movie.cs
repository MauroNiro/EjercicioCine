using System.Text.Json;


namespace EjercicioCine.Classes
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string? MovieName { get; set; }
        public int DirectorId { get; set; }
        public int Length { get; set; }
        public bool IsNational { get; set; }




        public static List<Movie> LoadMovie()
        {
            var movies = new List<Movie>();
            try
            {
                var sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\movies.txt");
                var sFilePath = Path.GetFullPath(sFile);
                var json = File.ReadAllText(sFilePath);
                movies = JsonSerializer.Deserialize<List<Movie>>(json);
            }
            catch (Exception)
            {
                Console.WriteLine("El archivo movies.txt no se pudo abrir");
            }

            return movies;
        }
    }

}


