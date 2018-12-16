using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Lab1.Models;
using Npgsql;
using Npgsql.NameTranslation;
using System.Data;

namespace Lab1.Controllers
{
    class FilmController
    {
        private static string connString = ConfigurationManager.ConnectionStrings["SampleDB"].ConnectionString;

        //private static readonly string CreateScript = @"CREATE TABLE IF NOT EXISTS films
        //                                                (
        //                                                    id              SERIAL PRIMARY KEY,
        //                                                    title           varchar(100) NOT NULL,
        //                                                    year            integer NOT NULL,
        //                                                    genre           film_genres,
        //                                                    description     text ,
        //                                                    is_released     boolean NOT NULL,
        //                                                    rating          integer
        //                                                );";

        //private static readonly string DropScript = @"DROP TABLE IF EXISTS films;";
        
        #region CRUD

        public static void Add(Film film)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO films (title, year, description, is_released, rating) 
                                        VALUES (@title, @year, @description, @is_released, @rating);";  
                    cmd.Parameters.AddWithValue("title", film.Title);
                    cmd.Parameters.AddWithValue("year", film.Year);
                    cmd.Parameters.AddWithValue("description", film.Description);
                    cmd.Parameters.AddWithValue("is_released", film.IsReleased);
                    cmd.Parameters.AddWithValue("rating", film.Rating);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Film> GetAll()
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"SELECT * FROM films", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    List<Film> films = new List<Film>();
                    while (reader.Read())
                    {
                        Film film = new Film(reader);
                        films.Add(film);
                    }

                    return films;
                }
            }
        }

        public static Film GetById(long id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM films WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Film film = null;

                        while (reader.Read())
                        {
                            film = new Film(reader);
                        }

                        return film;
                    }
                }
            }
        }
        
        public static bool Delete(long id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"DELETE FROM films WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool Update(long id, Film film)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE films 
                                        SET title = @title, year = @year, description = @description 
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("title", film.Title);
                    cmd.Parameters.AddWithValue("year", film.Year);
                    cmd.Parameters.AddWithValue("description", film.Description);
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<Film> GetFilmsWithPagination(uint page, uint limit)
        {
            uint start = (page * limit) - limit;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM films OFFSET @start LIMIT @limit";
                    cmd.Parameters.AddWithValue("start", (int)start);
                    cmd.Parameters.AddWithValue("limit", (int)limit);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Film> films = new List<Film>();
                        while (reader.Read())
                        {
                            Film film = new Film(reader);
                            films.Add(film);
                        }

                        return films;
                    }
                }
            }
        }

        #endregion

        #region FTS

        public static List<Film> GetByWord(string word, uint page, uint limit)
        {
            uint start = (page * limit) - limit;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT id, ts_headline(title, q) AS title, year, ts_headline(description, q) AS description, is_released, rating
                                        FROM films, plainto_tsquery(@str) AS q 
                                        WHERE make_tsvector(title, description) @@ q
                                        OFFSET @start LIMIT @limit;";
                    cmd.Parameters.AddWithValue("str", word);
                    cmd.Parameters.AddWithValue("start", (int)start);
                    cmd.Parameters.AddWithValue("limit", (int)limit);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Film> films = new List<Film>();
                        while (reader.Read())
                        {
                            Film film = new Film(reader);
                            films.Add(film);
                        }

                        return films;
                    }
                }
            }
        }

        public static List<Film> GetFilmsWhereThereIsNoWord(string word, uint page, uint limit)
        {
            uint start = (page * limit) - limit;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT id, ts_headline(title, q) AS title, year, ts_headline(description, q) AS description, is_released, rating
                                        FROM films, plainto_tsquery(@word) AS q 
                                        WHERE NOT make_tsvector(title, description) @@ q
                                        OFFSET @start LIMIT @limit;";
                    cmd.Parameters.AddWithValue("word", word);
                    cmd.Parameters.AddWithValue("start", (int)start);
                    cmd.Parameters.AddWithValue("limit", (int)limit);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Film> films = new List<Film>();
                        while (reader.Read())
                        {
                            Film film = new Film(reader);
                            films.Add(film);
                        }

                        return films;
                    }
                }
            }
        }

        #endregion

        public static int GetCount()
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"SELECT count(*) FROM public.films;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    int count = -1;
                    while (reader.Read())
                    {
                        count = (int)reader["count"];
                    }

                    return count;
                }
            }
        }

        public static List<Film> GetByRating(bool is_released, int min, int max)
        {
            if (min > max) throw new ArgumentException("Min > max");
            
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM films 
                                        WHERE is_released = @is_released AND (rating > @min AND rating < @max)";
                    cmd.Parameters.AddWithValue("is_released", is_released);
                    cmd.Parameters.AddWithValue("min", min);
                    cmd.Parameters.AddWithValue("max", max);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Film> films = new List<Film>();

                        while (reader.Read())
                        {
                            Film film = new Film();

                            film.Id = (int)reader["id"];
                            film.Title = (string)reader["title"];
                            film.Year = (int)reader["year"];
                            film.Description = (string)reader["description"];
                            film.IsReleased = (bool)reader["is_released"];
                            film.Rating = (int)reader["rating"];

                            films.Add(film);
                        }

                        return films;
                    }
                }

            }
        }

        #region ActorTo

        public static void AddActorTo(long film_id, long actor_id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO film_actor (film_id, actor_id) VALUES (@film_id, @actor_id)";
                    cmd.Parameters.AddWithValue("film_id", film_id);
                    cmd.Parameters.AddWithValue("actor_id", actor_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Actor> GetAllActorsById(long id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT fa.film_id, ac.* 
                                        FROM film_actor 
                                        AS fa 
                                        INNER JOIN actors
                                        AS ac
                                        ON fa.actor_id = ac.id 
                                        WHERE fa.film_id = @film_id; ";
                    cmd.Parameters.AddWithValue("film_id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Actor> actors = new List<Actor>();
                        while (reader.Read())
                        {
                            Actor actor = new Actor();

                            actor.Id = (long)reader["id"];
                            actor.FirstName = (string)reader["first_name"];
                            actor.LastName = (string)reader["last_name"];
                            actor.BirthDate = (DateTime)reader["birth_date"];

                            actors.Add(actor);
                        }

                        return actors;
                    }
                }
            }
        }

        public static bool DeleteActorFrom(long film_id, long actor_id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"DELETE FROM film_actor WHERE film_id = @film_id AND actor_id = @actor_id";
                    cmd.Parameters.AddWithValue("film_id", film_id);
                    cmd.Parameters.AddWithValue("actor_id", actor_id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        #endregion

        #region GenreTo

        public static void AddGenreTo(long film_id, long genre_id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO film_genre (film_id, genre_id) VALUES (@film_id, @genre_id)";
                    cmd.Parameters.AddWithValue("film_id", film_id);
                    cmd.Parameters.AddWithValue("genre_id", genre_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Genre> GetAllGenresById(long id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT fg.film_id, g.* 
                                        FROM film_genre 
                                        AS fg 
                                        INNER JOIN genres
                                        AS g
                                        ON fg.genre_id = g.id 
                                        WHERE fg.film_id = @film_id; ";
                    cmd.Parameters.AddWithValue("film_id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Genre> genres = new List<Genre>();
                        while (reader.Read())
                        {
                            Genre genre = new Genre(reader);
                            genres.Add(genre);
                        }

                        return genres;
                    }
                }
            }
        }

        public static bool DeleteGenreFrom(long film_id, long genre_id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"DELETE FROM film_genre WHERE film_id = @film_id AND genre_id = @genre_id";
                    cmd.Parameters.AddWithValue("film_id", film_id);
                    cmd.Parameters.AddWithValue("genre_id", genre_id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        #endregion

    }
}
