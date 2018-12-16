using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.Controllers;
using Lab1.Printer;
using Faker;
namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //FillDataBaseWithFakeFilms(30);
            //FillDataBaseWithFakeGenres(10);
            //FillDataBaseWithFakeActors(100);
            Console.Clear();
            Console.WriteLine("All Tables");

            bool exit = false;
            do
            {
                Console.WriteLine("1. Films");
                Console.WriteLine("2. Actors");
                Console.WriteLine("3. Genres");
                Console.WriteLine("0. Exit");

                Console.Write("Сhoose a table: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            Films();
                            break;
                        }
                    case "2":
                        {
                            Actors();
                            break;
                        } 
                    case "3":
                        {
                            Genres();
                            break;
                        };
                    case "0":
                        {
                            exit = true;
                            break;
                        };
                    default: break;
                }
            }
            while (!exit);


            Console.Write("End");
            Console.ReadKey();
        }

        #region Films

        static void Films()
        {
            Console.Clear();
            Console.WriteLine("FILMS");

            bool exit = false;
            do
            {
                Console.WriteLine("1. Get all films.");
                Console.WriteLine("2. Get film by id.");
                Console.WriteLine("3. Insert new film.");
                Console.WriteLine("4. Delete film.");
                Console.WriteLine("5. Update film.");
                Console.WriteLine("6. Get all films by word.");
                Console.WriteLine("7. Get all films where there is no word.");
                Console.WriteLine("8. Get all films by rating.");
                Console.WriteLine("9. Get all actors from film.");
                Console.WriteLine("10. Add actor to film by actor id.");
                Console.WriteLine("11. Delete actor from film.");
                Console.WriteLine("12. Get all genres from film.");
                Console.WriteLine("13. Add genre to film.");
                Console.WriteLine("14. Delete genre from film.");
                Console.WriteLine("0. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            //GetAllFilm();
                            GetFilmsWithPagination();
                            break;
                        }
                    case "2":
                        {
                            GetFilmById();
                            break;
                        }
                    case "3":
                        {
                            InsertFilm();
                            break;
                        }
                    case "4":
                        {
                            DeleteFilm();
                            break;
                        }
                    case "5":
                        {
                            UpdateFilm();
                            break;
                        }
                    case "6":
                        {
                            GetFilmsByWord();
                            break;
                        }
                    case "7":
                        {
                            GetFilmsWhereThereIsNoWord();
                            break;
                        }
                    case "8":
                        {
                            GetByRating();
                            break;
                        }
                    case "9":
                        {
                            GetAllActorsFromFilm();
                            break;
                        }
                    case "10":
                        {
                            AddActorToFilmById();
                            break;
                        }
                    case "11":
                        {
                            DeleteActorFromFilmById();
                            break;
                        }
                    case "12":
                        {
                            GetAllGenresFromFilm();
                            break;
                        }
                    case "13":
                        {
                            AddGenreToFilmById();
                            break;
                        }
                    case "14":
                        {
                            DeleteGenreFromFilmById();
                            break;
                        }
                    case "0":
                        {
                            exit = true;
                            break;
                        };
                    default: break;
                }
            }
            while (!exit);
        }

        static void GetAllFilm()
        {
            List<Film> films = FilmController.GetAll();

            TablePrinter<Film>.Print(films);
        }

        static void GetFilmById()
        {
            long id = GetId();
            if (id < 0) return;

            Film film = FilmController.GetById(id);

            if (film != null)
            {
                TablePrinter<Film>.Print(film);
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void InsertFilm()
        {
            Film film = GetFilm();

            FilmController.Add(film);

            Console.WriteLine("INSERT");
        }

        static void DeleteFilm()
        {
            long id = GetId();
            if (id < 0) return;

            if (FilmController.Delete(id))
            {
                Console.WriteLine("DELETE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void UpdateFilm()
        {
            long id = GetId();
            if (id < 0) return;

            Film film = GetFilm();

            if (FilmController.Update(id, film))
            {
                Console.WriteLine("UPDATE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }


        static void GetByRating()
        {
            Console.WriteLine("Min: ");
            string answer = Console.ReadLine();

            int min = Int32.Parse(answer);

            Console.WriteLine("Max: ");
            answer = Console.ReadLine();

            int max = Int32.Parse(answer);

            List<Film> films = FilmController.GetByRating(true, min, max);

            TablePrinter<Film>.Print(films);
        }

        static void GetAllActorsFromFilm()
        {
            long id = GetId();
            if (id < 0) return;

            List<Actor> actors = FilmController.GetAllActorsById(id);
            if (actors.Count > 0)
            {
                TablePrinter<Actor>.Print(actors);
            }
            else
            {
                Console.WriteLine("NO actors");
            }
        }

        static void AddActorToFilmById()
        {
            Console.Write("Film ");
            long film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Actor ");
            long actor_id = GetId();
            if (actor_id < 0) return;

            try
            {
                FilmController.AddActorTo(film_id, actor_id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        static void DeleteActorFromFilmById()
        {
            Console.Write("Film ");
            long film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Actor ");
            long actor_id = GetId();
            if (actor_id < 0) return;

            if (FilmController.DeleteActorFrom(film_id, actor_id))
            {
                Console.WriteLine("DELETE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void GetAllGenresFromFilm()
        {
            long id = GetId();
            if (id < 0) return;

            List<Genre> actors = FilmController.GetAllGenresById(id);
            if (actors.Count > 0)
            {
                TablePrinter<Genre>.Print(actors);
            }
            else
            {
                Console.WriteLine("NO Genres");
            }
        }

        static void AddGenreToFilmById()
        {
            Console.Write("Film: ");
            long film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Genre: ");
            long actor_id = GetId();
            if (actor_id < 0) return;

            try
            {
                FilmController.AddGenreTo(film_id, actor_id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        static void DeleteGenreFromFilmById()
        {
            Console.Write("Film ");
            long film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Genre ");
            long actor_id = GetId();
            if (actor_id < 0) return;

            if(FilmController.DeleteGenreFrom(film_id, actor_id))
            {
                Console.WriteLine("DELETE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }


        static void GetFilmsWithPagination()
        {
            uint page = 1;
            uint limit = 10;
            bool exit = false;
            do
            {
                Console.Clear();

                List<Film> films = FilmController.GetFilmsWithPagination(page, limit);
                TablePrinter<Film>.Print(films);

                Console.WriteLine("1. Prev");
                Console.WriteLine("2. Next");
                Console.WriteLine("3. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            if (page != 1)
                            {
                                page--;
                            }
                            break;
                        }
                    case "2":
                        {
                            page++;
                            break;
                        }
                    case "3":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            while (!exit);
        }

        static void GetFilmsByWord()
        {
            Console.Write("str: ");
            string word = Console.ReadLine();

            uint page = 1;
            uint limit = 10;
            bool exit = false;
            do
            {
                Console.Clear();

                List<Film> films = FilmController.GetByWord(word, page, limit);
                TablePrinter<Film>.Print(films);

                Console.WriteLine("1. Prev");
                Console.WriteLine("2. Next");
                Console.WriteLine("3. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            if (page != 1)
                            {
                                page--;
                            }
                            break;
                        }
                    case "2":
                        {
                            page++;
                            break;
                        }
                    case "3":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            while (!exit);
        }

        static void GetFilmsWhereThereIsNoWord()
        {
            Console.Write("str: ");
            string word = Console.ReadLine();

            uint page = 1;
            uint limit = 10;
            bool exit = false;
            do
            {
                Console.Clear();

                List<Film> films = FilmController.GetFilmsWhereThereIsNoWord(word, page, limit);
                TablePrinter<Film>.Print(films);

                Console.WriteLine("1. Prev");
                Console.WriteLine("2. Next");
                Console.WriteLine("3. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            if (page != 1)
                            {
                                page--;
                            }
                            break;
                        }
                    case "2":
                        {
                            page++;
                            break;
                        }
                    case "3":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            while (!exit);
        }

        private static Film GetFilm()
        {
            Console.Write("Title:");
            string title = Console.ReadLine();

            Console.Write("Year:");
            int year = Int32.Parse(Console.ReadLine());


            Console.Write("Description:");
            string description = Console.ReadLine();

            Film film = new Film();

            film.Title = title;
            film.Year = year;
            film.Description = description;

            return film;
        }

        #endregion

        #region Genres

        static void Genres()
        {
            Console.Clear();
            Console.WriteLine("GENRES");

            bool exit = false;
            do
            {
                Console.WriteLine("1. Get all genres.");
                Console.WriteLine("2. Get genre by id.");
                Console.WriteLine("3. Insert new genre.");
                Console.WriteLine("4. Delete genre.");
                Console.WriteLine("5. Update genre.");
                Console.WriteLine("0. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            GetGenresWithPagination();
                            break;
                        }
                    case "2":
                        {
                            GetGenreById();
                            break;
                        }
                    case "3":
                        {
                            InsertGenre();
                            break;
                        }
                    case "4":
                        {
                            DeleteGenre();
                            break;
                        }
                    case "5":
                        {
                            UpdateGenre();
                            break;
                        }
                    case "0":
                        {
                            exit = true;
                            break;
                        }
                    default: break;
                }
            }
            while (!exit);
        }

        static void GetGenresWithPagination()
        {
            uint page = 1;
            uint limit = 10;
            bool exit = false;
            do
            {
                Console.Clear();

                List<Genre> genres = GenreController.GetWithPagination(page, limit);
                TablePrinter<Genre>.Print(genres);

                Console.WriteLine("1. Prev");
                Console.WriteLine("2. Next");
                Console.WriteLine("0. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            if (page != 1)
                            {
                                page--;
                            }
                            break;
                        }
                    case "2":
                        {
                            page++;
                            break;
                        }
                    case "0":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            while (!exit);
        }

        static void GetGenreById()
        {
            long id = GetId();
            if (id < 0) return;

            Genre genre = GenreController.GetById(id);

            if (genre != null)
            {
                TablePrinter<Genre>.Print(genre);
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void InsertGenre()
        {
            Genre genre = GetGenre();

            GenreController.Add(genre);

            Console.WriteLine("INSERT");
        }

        static void DeleteGenre()
        {
            long id = GetId();
            if (id < 0) return;

            if (GenreController.Delete(id))
            {
                Console.WriteLine("DELETE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void UpdateGenre()
        {
            long id = GetId();
            if (id < 0) return;

            Genre genre = GetGenre();

            if (GenreController.Update(id, genre))
            {
                Console.WriteLine("UPDATE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        private static Genre GetGenre()
        {
            Console.Write("Name:");
            string name = Console.ReadLine();

            Console.Write("Description:");
            string description = Console.ReadLine();

            Genre genre = new Genre();

            genre.Name = name;
            genre.Description = description;

            return genre;
        }

        #endregion

        #region Actors

        static void Actors()
        {
            Console.Clear();
            Console.WriteLine("ACTORS");

            bool exit = false;
            do
            {
                Console.WriteLine("1. Get all actors.");
                Console.WriteLine("2. Get actor by id.");
                Console.WriteLine("3. Insert new actor.");
                Console.WriteLine("4. Delete actor.");
                Console.WriteLine("5. Update actor.");
                Console.WriteLine("0. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            GetActorsWithPagination();
                            break;
                        }
                    case "2":
                        {
                            GetActorById();
                            break;
                        }
                    case "3":
                        {
                            InsertActor();
                            break;
                        }
                    case "4":
                        {
                            DeleteActor();
                            break;
                        }
                    case "5":
                        {
                            UpdateActor();
                            break;
                        }
                    case "0":
                        {
                            exit = true;
                            break;
                        }
                    default: break;
                }
            }
            while (!exit);
        }

        static void GetActorsWithPagination()
        {
            uint page = 1;
            uint limit = 10;
            bool exit = false;
            do
            {
                Console.Clear();

                List<Actor> actors = ActorController.GetWithPagination(page, limit);
                TablePrinter<Actor>.Print(actors);

                Console.WriteLine("1. Prev");
                Console.WriteLine("2. Next");
                Console.WriteLine("0. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
                            if (page != 1)
                            {
                                page--;
                            }
                            break;
                        }
                    case "2":
                        {
                            page++;
                            break;
                        }
                    case "0":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            while (!exit);
        }

        static void GetActorById()
        {
            long id = GetId();
            if (id < 0) return;

            Actor actor = ActorController.GetById(id);

            if (actor != null)
            {
                TablePrinter<Actor>.Print(actor);
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void InsertActor()
        {
            Actor actor = GetActor();

            ActorController.Add(actor);

            Console.WriteLine("INSERT");
        }

        static void DeleteActor()
        {
            long id = GetId();
            if (id < 0) return;

            if (ActorController.Delete(id))
            {
                Console.WriteLine("DELETE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void UpdateActor()
        {
            long id = GetId();
            if (id < 0) return;

            Actor actor = GetActor();

            if (ActorController.Update((int)id, actor))
            {
                Console.WriteLine("UPDATE");
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        private static Actor GetActor()
        {
            Console.Write("First name:");
            string fname = Console.ReadLine();

            Console.Write("Last name:");
            string lname = Console.ReadLine();

            Actor actor = new Actor();

            actor.FirstName = fname;
            actor.LastName = lname;
            actor.BirthDate = new DateTime();

            return actor;
        }

        #endregion

        private static long GetId()
        {
            Console.Write("id: ");
            string answer = Console.ReadLine();

            try
            {
                ulong id = UInt64.Parse(answer);
                return (long)id;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Incorrect input.");
                return -1;
            } 
        }
        
        static void FillDataBaseWithFakeFilms(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                FilmController.Add(Film.GetRandomFilm());
            }
        }

        static void FillDataBaseWithFakeActors(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                ActorController.Add(Actor.GetRandomActor());
            }
        }

        static void FillDataBaseWithFakeGenres(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                GenreController.Add(Genre.GetRandomGenre());
            }
        }
    }
}
