using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EjercicioCine.Classes
{
    internal class Movie
    {
        public int MovieId { get; set; }
        public string? MovieName { get; set; }
        public int DirectorId { get; set; }
        public int Length { get; set; }
        public bool IsNational { get; set; }




        public static List<Movie> LoadPelicula()
        {
            List<Movie>? movies = new List<Movie>();
            try
            {
                string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\movies.txt");
                string sFilePath = Path.GetFullPath(sFile);
                string json = File.ReadAllText(sFilePath);
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


