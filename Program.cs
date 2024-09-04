using EjercicioCine.Classes;
using System.IO;
using System.Reflection;
Show show = new Show();
List<Show> shows = new List<Show>();
Console.WriteLine("Bienvenido al Cine Demo");
List<Director> directors =  Director.LoadDirector();
List<Movie> movies =Movie.LoadMovie();
int option = 0;


//Menu
while (option != 5)
{
    Console.WriteLine("1.Añadir nueva funcion.");
    Console.WriteLine("2.Editar una funcion.");
    Console.WriteLine("3.Eliminar una funcion.");
    Console.WriteLine("4.Ver las funciones actuales");
    Console.WriteLine("5.Cerrar aplicacion.");
    string? read = Console.ReadLine();
    bool isInt = int.TryParse(read, out option);
    switch (option)
    {
        case 1:
            shows = show.AddShow(movies, directors,shows);
            break;
        case 2:
            shows = show.EditShow(shows,movies,directors);
            break;
        case 3:
            shows = show.DeleteShow(shows);
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