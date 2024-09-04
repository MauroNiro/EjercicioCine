using EjercicioCine.Classes;
var show = new Show();
var shows = new List<Show>();
var directors =  Director.LoadDirector();
var movies =Movie.LoadMovie();
var option = 0;

Console.WriteLine("Bienvenido al Cine Demo");
//Menu
while (option != 5)
{
    Console.WriteLine("1.Añadir nueva funcion.");
    Console.WriteLine("2.Editar una funcion.");
    Console.WriteLine("3.Eliminar una funcion.");
    Console.WriteLine("4.Ver las funciones actuales");
    Console.WriteLine("5.Cerrar aplicacion.");
    var read = Console.ReadLine();
    int.TryParse(read, out option);
    switch (option)
    {
        case 1:
            shows = show.AddShow(movies, directors,shows);
            break;
        case 2:
            if (shows.Count > 0)
                shows = show.EditShow(shows, movies, directors);
            else
                Console.WriteLine("No hay funciones para editar.");
            break;
        case 3:
            if (shows.Count > 0)
                shows = show.DeleteShow(shows);
            else
                Console.WriteLine("No hay funciones para borrar.");
            break;
        case 4:
            Show.GetShows(shows);
            break;
        case 5:
            break;
        default:
            Console.WriteLine("Por favor elegi una opcion entre 1 y 5");
            break;
    }
}