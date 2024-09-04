﻿using System;
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
                string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\directors.txt");
                string sFilePath = Path.GetFullPath(sFile);
                string json = File.ReadAllText(sFilePath);
                Directors = JsonSerializer.Deserialize<List<Director>>(json);
                return Directors;
            }
            catch (Exception)
            {
                Console.WriteLine("El archivo directors.txt no se pudo abrir.");
            }

            return null;

        }
    }
}
