using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace EjercicioCine.Classes
{
    public class Director
    {
        public int DirectorId { get; set; }
        public string? DirectorName { get; set; }


        public static List<Director> LoadDirector()
        {

            List<Director>? Directors = new List<Director>();
            try
            {
                string json = File.ReadAllText("F:\\Primero\\EjCine\\EjercicioCine\\directors.txt");
                Directors = JsonSerializer.Deserialize<List<Director>>(json);
                return Directors;
            }
            catch (Exception)
            {
                Console.WriteLine("File directors.txt could not open.");
            }

            return null;

        }
    }
}
