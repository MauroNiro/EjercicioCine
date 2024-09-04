

namespace EjercicioCine.Classes
{
    public class Show
    {
        public int ShowId { get; set; }
        public int MovieId { get; set; }
        public string? MovieName { get; set; }
        public int DirectorId { get; set; }
        public string? DirectorName { get; set; }
        public DateTime DateTime { get; set; }
        public int Price { get; set; }


        public List<Show> AddShow(List<Movie> movies, List<Director> directors, List<Show> shows)
        {
            var dateTime = new DateTime();
            var isSuccesfulDate = false;
            var isSuccesfulQuantity = false;
            var isSuccesfulPrice = false;
            var price = 0;
            Console.Clear();
            try
            {
                while (!isSuccesfulPrice)
                {
                    var movieFound = FindMovie(movies);

                    if (movieFound != null)
                    {
                        while (!isSuccesfulPrice)//Its only this bool because its the last one checked.
                        {
                            if (!isSuccesfulDate)
                            {
                                Console.WriteLine("Por favor ingresa que dia del mes seleccionado va a estar esta funcion");
                                var daystr = Console.ReadLine();
                                Console.WriteLine("Ahora ingresa en que mes va a estar esta funcion");
                                var monthstr = Console.ReadLine();
                                if (ValidateDate(daystr, monthstr))
                                {
                                    Console.WriteLine("Ahora ingresa la hora que va a estar esta funcion en un formato hh:mm");
                                    var timestr = Console.ReadLine();
                                    if (isSuccesfulDate = ValidateTime(daystr, monthstr, timestr))
                                        dateTime = Convert.ToDateTime($"{monthstr}/{daystr}/{DateTime.Now.Year} {timestr}");
                                }
                            }
                            if (isSuccesfulDate && !isSuccesfulQuantity)
                                isSuccesfulQuantity = CheckQuantity(shows, dateTime, movieFound);
                            if (isSuccesfulQuantity)
                            {
                                Console.WriteLine("Ahora por ultimo indica el precio de la funcion");
                                var pricestr = Console.ReadLine();
                                if (isSuccesfulPrice = ValidatePrice(pricestr))
                                    price = int.Parse(pricestr);
                            }
                            else isSuccesfulDate = false;
                        }
                        //Loads the show
                        var show = new Show();
                        show.MovieId = movieFound.MovieId;
                        show.Price = price;
                        show.DateTime = dateTime;
                        show.ShowId = shows.Count() + 1;
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

                return shows;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return shows;
            }
        }
        public List<Show> EditShow(List<Show> shows, List<Movie> movies, List<Director> directors)
        {
            Console.Clear();
            try
            {
                var isSuccesfulDate = false;
                var isSuccesfulQuantity = false;
                var isSuccesfulPrice = false;
                var dateTime = new DateTime();
                var price = 0;
                var id = 0;
                while (!isSuccesfulPrice)
                {
                    Console.WriteLine("Elegi que Show queres editar escribiendo su Id");
                    Console.WriteLine("Si queres ver cuales show estan ya programados envia 0");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        //Verifies if asked to see wich shows are there
                        if (id == 0)
                        {
                            GetShows(shows);
                        }
                        else
                        {
                            var foundShow = shows.Where(show => show.ShowId == id).FirstOrDefault();
                            if (foundShow != null)
                            {
                                var movieFound = FindMovie(movies);
                                if (movieFound != null)
                                {
                                    while (!isSuccesfulPrice)// Its only this bool because its the last one checked.
                                    {
                                        if (!isSuccesfulDate)
                                        {
                                            Console.WriteLine("Por favor ingresa que dia del mes seleccionado va a estar esta funcion");
                                            var daystr = Console.ReadLine();
                                            Console.WriteLine("Ahora ingresa en que mes va a estar esta funcion");
                                            var monthstr = Console.ReadLine();
                                            if (ValidateDate(daystr, monthstr))
                                            {
                                                Console.WriteLine("Ahora ingresa la hora que va a estar esta funcion en un formato hh:mm");
                                                var timestr = Console.ReadLine();
                                                if (isSuccesfulDate = ValidateTime(daystr, monthstr, timestr))
                                                    dateTime = Convert.ToDateTime($"{monthstr}/{daystr}/{DateTime.Now.Year} {timestr}");
                                            }
                                        }
                                        if (isSuccesfulDate && !isSuccesfulQuantity)
                                        {
                                            if (movieFound.MovieId != foundShow.MovieId || dateTime.Date != foundShow.DateTime.Date)
                                            {
                                                isSuccesfulQuantity = CheckQuantity(shows, dateTime, movieFound);
                                            }
                                            else isSuccesfulQuantity = true;
                                        }
                                        if (isSuccesfulQuantity)
                                        {
                                            Console.WriteLine("Ahora por ultimo indica el precio de la funcion");
                                            var pricestr = Console.ReadLine();
                                            if (isSuccesfulPrice = ValidatePrice(pricestr))
                                                price = int.Parse(pricestr);
                                        }
                                        else isSuccesfulDate = false;
                                    }
                                    //loads new data
                                    foundShow.MovieId = movieFound.MovieId;
                                    foundShow.Price = price;
                                    foundShow.DateTime = dateTime;
                                    foundShow.MovieName = movieFound.MovieName;
                                    foundShow.DirectorId = movieFound.DirectorId;
                                    foundShow.DirectorName = GetDirectorName(movieFound, directors);
                                    Console.WriteLine($"Editaste una funcion con la pelicula ID: {foundShow.MovieId} a las {foundShow.DateTime} al precio de {foundShow.Price}");
                                    Console.WriteLine("Presiona cualquier tecla para continuar.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    return shows;
                                }
                                else Console.WriteLine("No se encuentra esa pelicula.");
                            }
                            else Console.WriteLine("No se encuentra esa funcion");
                        }

                    }

                }
                return shows;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);
                return shows;
            }
        }
        public List<Show> DeleteShow(List<Show> shows)
        {
            Console.Clear();
            var idExists = false;
            while (!idExists)
            {
                Console.WriteLine("Por favor selecciona la ID de la funcion a borrar");
                Console.WriteLine("Si queres ver las funciones disponibles ingresa 0");
                if (int.TryParse(Console.ReadLine(), out var id))
                {
                    if (id == 0)
                    {
                        GetShows(shows);
                    }
                    else
                    {
                        if (shows.Any(show => show.ShowId == id))
                        {
                            idExists = true;
                            shows.RemoveAll(show => show.ShowId == id);
                            Console.WriteLine($"Eliminaste la funcion ID: {id}");
                            Console.WriteLine("Presiona cualquier tecla para continuar.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else Console.WriteLine("El ID ingresado no corresponde a ninguna funcion.");
                    }
                }
            }
            return shows;
        }
        private Movie FindMovie(List<Movie> movies)
        {
            var movieValid = false;
            var movieFound = new Movie();
            while (!movieValid)
            {
                Console.WriteLine("Por favor ingresa el ID de la pelicula que se va a ver en la nueva funcion.");
                Console.WriteLine("Si queres podes mirar los ID enviando 0.");
                if (int.TryParse(Console.ReadLine(), out var id))
                {
                    ;
                    // Displays the movies loaded
                    if (id == 0)
                    {
                        LogMovies(movies);
                    }
                    else
                    {
                        movieFound = movies.Where(movie => movie.MovieId == id).FirstOrDefault();
                        if (movieFound != null)
                        {
                            movieValid = true;
                        }
                    }
                }
            }
            return movieFound;
        }
        private void LogMovies(List<Movie> movies)
        {
            if (movies.Count > 0)
            {
                foreach (var movie in movies)
                    Console.WriteLine($"La pelicula {movie.MovieName} tiene ID:{movie.MovieId}");
            }
        }
        public static void GetShows(List<Show> shows)
        {
            if (shows.Count > 0)
            {
                foreach (var show in shows)
                    Console.WriteLine($"Hay un show a las {show.DateTime} con ID: {show.ShowId} pelicula: {show.MovieName} y director: {show.DirectorName} y precio {show.Price}");
            }
            else
            {
                Console.WriteLine("No hay funciones");
            }
        }
        //asks and add the DateTime values
        private bool ValidateDate(string daystr, string monthstr)
        {
            if (DateTime.TryParse($"{monthstr}/{daystr}/{DateTime.Now.Year}", out var date))
            {
                if (date < DateTime.Now.Date)
                {
                    Console.WriteLine("La fecha no puede ser anterior al dia de hoy");
                    return false;
                }
                return true;
            }
            else Console.WriteLine("Fecha no valida");
            return false;
                
        }
        private bool ValidateTime(string daystr, string monthstr, string timestr)
        {
            Console.WriteLine($"{monthstr}/{daystr}/{DateTime.Now.Year} {timestr}");
            if (DateTime.TryParse($"{monthstr}/{daystr}/{DateTime.Now.Year} {timestr}", out var datetime)){ 
                if (datetime < DateTime.Now)
                {
                    Console.WriteLine("El horario no puede ser anterior a el actual.");
                    return false;
                }
                return true;
            }
            else Console.WriteLine("Fecha no valida");
            return false;
        }
        private string GetDirectorName(Movie movie,List<Director>directors)
        {
            var directorName = directors.Where(director => director.DirectorId == movie.DirectorId).Select(director => director.DirectorName).FirstOrDefault();
            if (directorName == null)
                return "No se pudo obtener el nombre del director";
            return directorName;
        }
        private bool ValidatePrice(string pricestr)
        {            
            if (int.TryParse(pricestr, out var price))
            {
                if (price < 0)
                {
                    Console.WriteLine("El valor no puede ser menor a 0");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                Console.WriteLine("El valor debe ser numerico");
                return false;
            }
        }
        //Checks the quantity of functions are within expected limits
        private bool CheckQuantity(List<Show> shows, DateTime dateTime, Movie movie)
        {
            var cantShowsMovie = shows
                                .Where(show => show.MovieId == movie.MovieId && show.DateTime.Date == dateTime.Date).Count();
            if (cantShowsMovie >= 8 && !movie.IsNational)
            {
                Console.WriteLine("Ya llego al limite de funciones de esta pelicula por este dia.");
                return false;
            }
            else {
                var cantShowsDirector = shows.Where(show => show.DirectorId == movie.DirectorId && show.DateTime.Date == dateTime.Date).Count();
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


