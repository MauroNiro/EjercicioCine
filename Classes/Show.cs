using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EjercicioCine.Classes
{
    internal class Show
    {
        public int ShowId { get; set; }
        public int MovieId { get; set; }
        public string? MovieName { get; set; }
        public int DirectorId { get; set; }
        public string? DirectorName {  get; set; } 
        public DateTime DateTime { get; set; }
        public int Price { get; set; }


        public List<Show> AddShow(List<Movie> movies, List<Director> directors, List<Show> shows)
        {
            DateTime dateTime = new DateTime();
            bool isSuccesfulDate = false;
            bool isSuccesfulQuantity = false;
            bool isSuccesfulPrice = false;
            int id;
            int price = 0;
            Console.Clear();
            while (!isSuccesfulPrice)
            {
                Show show = new Show();
                Console.WriteLine("Por favor ingresa el ID de la pelicula que se va a ver en la nueva funcion.");
                Console.WriteLine("Si queres podes mirar los ID enviando 0.");
                int.TryParse(Console.ReadLine(), out id);
                // Muestra las peliculas cargadas
                if (id == 0)
                {
                    show.GetMovies(movies);
                }
                // Busca pelicula de el id mencionado y pide datos para hacer la carga
                else
                {
                    Movie? movieFound = movies.Where(movie => movie.MovieId == id).FirstOrDefault();

                    if (movieFound != null)
                    {
                        while (!isSuccesfulPrice)//es solo el ultimo por que necesita de los demas.
                        {
                            (isSuccesfulDate, dateTime) = show.InsertDate();
                            if (isSuccesfulDate)
                                isSuccesfulQuantity = show.CheckQuantity(shows, dateTime, movieFound);
                            if (isSuccesfulQuantity)
                                (isSuccesfulPrice, price) = show.InsertPrice();
                        }
                        //Hace la carga

                        show.MovieId = id;
                        show.Price = price;
                        show.DateTime = dateTime;
                        show.ShowId = shows.Count()+1;
                        show.MovieName = movieFound.MovieName;
                        show.DirectorId = movieFound.DirectorId;
                        show.DirectorName = show.GetDirectorName(movieFound, directors);

                        shows.Add(show);
                        Console.WriteLine($"Creaste una funcion de la pelicula ID: {show.MovieId} a las {show.DateTime} al precio de {show.Price}");
                        Console.WriteLine("Presiona cualquier tecla para continuar.");
                        Console.ReadKey();
                        Console.Clear();
                        return shows;
                    }
                    else Console.WriteLine("No se encuentra esa pelicula.");
                }
            }
            return shows;
        }
        public List<Show> EditShow(List<Show> shows, List<Movie> movies, List<Director>directors)
        {
            Console.Clear();
            bool isSuccesfulDate = false;
            bool isSuccesfulQuantity = false;
            bool isSuccesfulPrice = false;
            DateTime dateTime = new DateTime();
            int price = 0;
            int id;
            int movieId;
            while (!isSuccesfulPrice)
            {
                Console.WriteLine("Elegi que Show queres editar escribiendo su Id");
                Console.WriteLine("Si queres ver cuales show estan ya programados envia 0");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    //verifica si pidio ver cuales hay
                    if (id == 0)
                    {
                        show.GetShows(shows);
                    }
                    else
                    {
                        Show? foundShow = shows.Where(show => show.ShowId == id).FirstOrDefault();
                        if (foundShow != null)
                        {
                            Console.WriteLine("Por favor ingresa el ID de la pelicula que se va a ver en la funcion.");
                            Console.WriteLine("Si queres podes chequear los ID enviando 0.");
                            int.TryParse(Console.ReadLine(), out movieId);
                            // Muestra las peliculas cargadas
                            if (movieId == 0)
                            {
                                foundShow.GetMovies(movies);
                            }
                            // Busca pelicula de el id mencionado y pide datos para hacer la carga
                            else
                            {
                                Movie? movieFound = movies.Where(movie => movie.MovieId == movieId).FirstOrDefault();
                                if (movieFound != null)
                                {
                                    while (!isSuccesfulPrice)// es solo el ultimo por que necesita de los demas.
                                    {
                                        (isSuccesfulDate, dateTime) = foundShow.InsertDate();
                                        if (isSuccesfulDate && (movieId != foundShow.MovieId || dateTime.Day != foundShow.DateTime.Day))
                                        {
                                            isSuccesfulQuantity = foundShow.CheckQuantity(shows, dateTime, movieFound);
                                        }
                                        else isSuccesfulQuantity = true;
                                        if (isSuccesfulQuantity)
                                            (isSuccesfulPrice, price) = foundShow.InsertPrice();
                                    }
                                    //edita los datos
                                    foundShow.MovieId = movieFound.MovieId;
                                    foundShow.Price = price;
                                    foundShow.DateTime = dateTime;
                                    foundShow.MovieName = movieFound.MovieName;
                                    foundShow.DirectorId = movieFound.DirectorId;
                                    foundShow.DirectorName = foundShow.GetDirectorName(movieFound, directors);
                                    return shows;
                                }
                            }
                        }
                        else Console.WriteLine("No se encuentra esa pelicula.");
                    }
                }                
            }
            return shows;
        }
        public List<Show> DeleteShow(List<Show> shows)
        {
            Show show = new Show();
            bool idExists = false;
            while (!idExists)
            {   
                Console.WriteLine("Por favor selecciona la ID de la funcion a borrar");
                Console.WriteLine("Si queres ver las funciones disponibles ingresa 0");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (id == 0)
                    {
                        show.GetShows(shows);
                    }
                    else
                    {
                        if (shows.Any(show => show.ShowId == id))
                        {
                            idExists = true;
                            shows.RemoveAll(show => show.ShowId == id);
                        }
                        else Console.WriteLine("El ID ingresado no corresponde a ninguna funcion.");
                    }                    
                }
            }
            return shows;
        }
        public void GetMovies(List<Movie> movies)
        {
            if (movies.Count > 0)
            {
                foreach (Movie movie in movies)
                    Console.WriteLine($"La pelicula {movie.MovieName} tiene ID:{movie.MovieId.ToString()}");
            }
        }
        
        public void GetShows(List<Show> shows)
        {
            if (shows.Count > 0)
            {
                foreach (Show show in shows)
                    Console.WriteLine($"Hay un show a las {show.DateTime.ToString()} con ID: {show.ShowId.ToString()} pelicula: {show.MovieName} y director: {show.DirectorName}");
            }
        }
        //pide y insera los valores de fecha para no repetir codigo.
        public (bool, DateTime) InsertDate()
        {
            int day = 0;
            int month = 0;
            TimeOnly time = new TimeOnly();
            DateTime dateTime = new DateTime();
            Console.WriteLine("Por favor ingresa que dia del mes seleccionado va a estar esta funcion");
            string? daystr = Console.ReadLine();            
            if (int.TryParse(daystr, out  day) && day <= 31 && day >0)
            {
                Console.WriteLine("Ahora ingresa en que mes va a estar esta funcion");
                string? monthstr = Console.ReadLine();
                if (int.TryParse(monthstr, out  month) && month <= 12 && month > 1)
                {
                    Console.WriteLine("Ahora ingresa la hora que va a estar esta funcion en un formato hh:mm");
                    string? timestr = Console.ReadLine();
                    if (TimeOnly.TryParse(timestr, out  time))
                    {
                        if (DateTime.TryParse($"{month}/{day}/{DateTime.Now.Year} {time}", out dateTime))
                        {
                            if (dateTime < DateTime.Today)
                            {
                                Console.WriteLine("La fecha no puede ser anterior a hoy");
                                return (false, dateTime);
                            }
                            return (true, dateTime);
                        }
                        else
                        {
                            Console.WriteLine("Fecha no valida.");
                            return (false, dateTime);
                        }
                    }
                    else
                        Console.WriteLine("Horario no valido");
                }
                else
                    Console.WriteLine("Mes no valido");
            }
            else
            {
                Console.WriteLine("Dia no Valido");
            }
            return (false, dateTime);
        }
        public string? GetDirectorName(Movie movie,List<Director>directors)
        {            
           return directors.Where(director=> director.DirectorId == movie.DirectorId).Select(director=> director.DirectorName).FirstOrDefault();
        }
        public (bool, int) InsertPrice()
        {
            Console.WriteLine("Ahora por ultimo indica el precio de la funcion");
            if (int.TryParse(Console.ReadLine(), out int price))
            {
                if (price < 0)
                {
                    Console.WriteLine("El valor no puede ser menor a 0");
                    return (false, price);
                }
                else
                    return (true, price);
            }
            else
            {
                Console.WriteLine("El valor debe ser numerico");
                return (false, 0);
            }
        }
        //Chequea que la cantidad de funciones este dentro de los limites esperados.
        public bool CheckQuantity(List<Show> shows, DateTime dateTime, Movie movie)
        {
            int cantShowsMovie = shows
                                .Where(show => show.MovieId == movie.MovieId && show.DateTime.Day == dateTime.Day).Count();
            if (cantShowsMovie >= 8 && !movie.IsNational)
            {
                Console.WriteLine("Ya llego al limite de funciones de esta pelicula por este dia.");
                return false;
            }
            else {
                int cantShowsDirector = shows.Where(show => show.DirectorId == movie.DirectorId && show.DateTime.Day == dateTime.Day).Count();
                if (cantShowsDirector >= 10)
                {
                    Console.WriteLine("Ya llego al limite de funciones de este director por este dia.");
                    return false;
                }
            }
             return true;
        }
    }
}


