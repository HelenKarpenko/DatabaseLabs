using Lab2.Context;
using Lab2.Entities;
using Lab2.Printer;
using Lab2.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Lab2
{
    class Program
    {
        private static UnitOfWork database;

        public static void Init()
        {
            Program.database = new UnitOfWork("SampleDB");
        }

        static void Main(string[] args)
        {
            Init();
            Console.Clear();
            //FillDataBaseWithFakeFilms(20);
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
                Console.WriteLine("6. Get all films by rating.");
                Console.WriteLine("7. Get all actors from film.");
                Console.WriteLine("8. Add actor to film by actor id.");
                Console.WriteLine("9. Delete actor from film.");
                Console.WriteLine("10. Get all genres from film.");
                Console.WriteLine("11. Add genre to film.");
                Console.WriteLine("12. Delete genre from film.");
                Console.WriteLine("0. Exit.");

                Console.Write("Сhoose a comm: ");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        {
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
                            GetByRating();
                            break;
                        }
                    case "7":
                        {
                            GetAllActorsFromFilm();
                            break;
                        }
                    case "8":
                        {
                            AddActorToFilmById();
                            break;
                        }
                    case "9":
                        {
                            DeleteActorFromFilmById();
                            break;
                        }
                    case "10":
                        {
                            GetAllGenresFromFilm();
                            break;
                        }
                    case "11":
                        {
                            AddGenreToFilmById();
                            break;
                        }
                    case "12":
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

        static void GetFilmById()
        {
            int id = GetId();
            if (id < 0) return;

            Film film = database.films.Get(id);


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
            database.films.Create(film);
            database.Save();

            Console.WriteLine("INSERT");
        }

        static void DeleteFilm()
        {
            int id = GetId();
            if (id < 0) return;

            Film film = database.films.Delete(id);
            database.Save();

            if (film != null)
            {
                Console.WriteLine("DELETE");
                TablePrinter<Film>.Print(film);
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void UpdateFilm()
        {
            int id = (int)GetId();
            if (id < 0) return;

            Film film = GetFilm();
            film.Id = id;

            Film update = database.films.Update(film);
            database.Save();

            if (update != null)
            {
                Console.WriteLine("UPDATE");
                TablePrinter<Film>.Print(film);
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

            List<Film> films = database.films.Find(x => x.Rating >= min && x.Rating <= max).ToList();

            TablePrinter<Film>.Print(films);
        }

        static void GetAllActorsFromFilm()
        {
            int id = GetId();
            if (id < 0) return;

            List<int> ids = database.FilmActor.Find(x => x.FilmId == id).Select(x => x.ActorId).ToList();

            List<Actor> actors = new List<Actor>();
            foreach (int aid in ids)
            {
                actors.Add(database.actors.Get(aid));
            }

            if (actors.Count > 0)
            {
                TablePrinter<Actor>.Print(actors);
            }
            else
            {
                Console.WriteLine("NO Actor");
            }
        }

        static void AddActorToFilmById()
        {
            Console.Write("Film ");
            int film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Actor ");
            int actor_id = GetId();
            if (actor_id < 0) return;

            try
            {
                FilmActor filmActor = new FilmActor
                {
                    FilmId = film_id,
                    Film = database.films.Get(film_id),
                    ActorId = actor_id,
                    Actor = database.actors.Get(actor_id),
                };

                database.FilmActor.Create(filmActor);
                database.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void DeleteActorFromFilmById()
        {
            Console.Write("Film ");
            int film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Actor ");
            int actor_id = GetId();
            if (actor_id < 0) return;

            try
            {
                var result = database.FilmActor.Delete(film_id, actor_id);
                if (result != null)
                {
                    Console.WriteLine("DELETE");
                }
                else
                {
                    Console.WriteLine("NO RECORD");
                }
                database.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        static void GetAllGenresFromFilm()
        {
            int id = GetId();
            if (id < 0) return;

            List<int> ids = database.FilmGenre.Find(x => x.FilmId == id).Select(x => x.GenreId).ToList();

            List<Genre> genres = new List<Genre>();
            foreach (int gid in ids)
            {
                genres.Add(database.genres.Get(gid));
            }

            if (genres.Count > 0)
            {
                TablePrinter<Genre>.Print(genres);
            }
            else
            {
                Console.WriteLine("NO Genres");
            }
        }

        static void AddGenreToFilmById()
        {
            Console.Write("Film: ");
            int film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Genre: ");
            int actor_id = GetId();
            if (actor_id < 0) return;

            try
            {
                FilmGenre filmGenre = new FilmGenre
                {
                    FilmId = film_id,
                    Film = database.films.Get(film_id),
                    GenreId = actor_id,
                    Genre = database.genres.Get(actor_id),
                };

                database.FilmGenre.Create(filmGenre);
                database.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void DeleteGenreFromFilmById()
        {
            Console.Write("Film ");
            int film_id = GetId();
            if (film_id < 0) return;

            Console.Write("Genre ");
            int actor_id = GetId();
            if (actor_id < 0) return;

            try
            {
                var result = database.FilmGenre.Delete(film_id, actor_id);
                if (result != null)
                {
                    Console.WriteLine("DELETE");
                }
                else
                {
                    Console.WriteLine("NO RECORD");
                }
                database.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void GetFilmsWithPagination()
        {
            int page = 1;
            int limit = 2;
            bool exit = false;
            do
            {
                Console.Clear();
                int start = (page * limit) - limit;
                List<Film> films = database.films.GetAll().OrderBy(x => x.Id).Skip(start).Take(limit).ToList();
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

            Film film = new Film
            {
                Title = title,
                Year = year,
                Description = description
            };

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
            int page = 1;
            int limit = 10;
            bool exit = false;
            do
            {
                Console.Clear();
                int start = (page * limit) - limit;
                List<Genre> genres = database.genres.GetAll().OrderBy(x => x.Id).Skip(start).Take(limit).ToList();
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
            int id = GetId();
            if (id < 0) return;

            Genre genre = database.genres.Get(id);

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

            database.genres.Create(genre);
            database.Save();
            TablePrinter<Genre>.Print(genre);

            Console.WriteLine("INSERT");
        }

        static void DeleteGenre()
        {
            int id = GetId();
            if (id < 0) return;

            Genre genre = database.genres.Delete(id);
            database.Save();

            if (genre != null)
            {
                Console.WriteLine("DELETE");
                TablePrinter<Genre>.Print(genre);
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void UpdateGenre()
        {
            int id = GetId();
            if (id < 0) return;

            Genre genre = GetGenre();
            genre.Id = id;
            Genre result = database.genres.Update(id, genre);
            database.Save();

            if (genre != null)
            {
                Console.WriteLine("UPDATE");
                TablePrinter<Genre>.Print(result);
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

            Genre genre = new Genre
            {
                Name = name,
                Description = description
            };

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
            int page = 1;
            int limit = 10;
            bool exit = false;
            do
            {
                Console.Clear();
                int start = (page * limit) - limit;
                List<Actor> actors = database.actors.GetAll().OrderBy(x => x.Id).Skip(start).Take(limit).ToList();
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
            int id = GetId();
            if (id < 0) return;

            Actor actor = database.actors.Get(id);

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

            Actor result = database.actors.Create(actor);
            database.Save();

            Console.WriteLine("INSERT");
            TablePrinter<Actor>.Print(result);
        }

        static void DeleteActor()
        {
            int id = GetId();
            if (id < 0) return;

            Actor result = database.actors.Delete(id);
            database.Save();

            if (result != null)
            {
                Console.WriteLine("DELETE");
                TablePrinter<Actor>.Print(result);
            }
            else
            {
                Console.WriteLine("NO RECORD");
            }
        }

        static void UpdateActor()
        {
            int id = GetId();
            if (id < 0) return;

            Actor actor = GetActor();
            actor.Id = id;
            Actor result = database.actors.Update(id, actor);
            database.Save();

            if (result != null)
            {
                Console.WriteLine("UPDATE");
                TablePrinter<Actor>.Print(result);
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

            Actor actor = new Actor
            {
                FirstName = fname,
                LastName = lname,
                BirthDate = new DateTime()
            };

            return actor;
        }

        #endregion

        private static int GetId()
        {
            Console.Write("id: ");
            string answer = Console.ReadLine();

            try
            {
                int id = Int32.Parse(answer);
                return id;
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
                database.films.Create(Film.GetRandom());
            }
            database.Save();
        }

        static void FillDataBaseWithFakeActors(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                database.actors.Create(Actor.GetRandom());
            }
            database.Save();
        }

        static void FillDataBaseWithFakeGenres(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                database.genres.Create(Genre.GetRandom());
            }
            database.Save();
        }
    }
}
