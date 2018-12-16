using Npgsql;
using System;
using System.Collections.Generic;
using Lab1.Models;
using System.Configuration;

namespace Lab1.Controllers
{
    class ActorController
    {
        private static string connString = ConfigurationManager.ConnectionStrings["SampleDB"].ConnectionString;

        #region CRUD

        public static void Add(Actor actor)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO actors (first_name, last_name, birth_date) 
                                        VALUES (@first_name, @last_name, @birth_date)";
                    cmd.Parameters.AddWithValue("first_name", actor.FirstName);
                    cmd.Parameters.AddWithValue("last_name", actor.LastName);
                    cmd.Parameters.AddWithValue("birth_date", actor.BirthDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Actor> GetAll()
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"SELECT * FROM actors", conn))
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

        public static Actor GetById(long id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM actors WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Actor actor = new Actor();
                        while (reader.Read())
                        {
                            actor.Id = (long)reader["id"];
                            actor.FirstName = (string)reader["first_name"];
                            actor.LastName = (string)reader["last_name"];
                            actor.BirthDate = (DateTime)reader["birth_date"];
                        }

                        return actor;
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
                    cmd.CommandText = @"DELETE FROM actors WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery() > 0; 
                }
            }
        }

        public static bool Update(int id, Actor actor)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE actors 
                                        SET first_name = @first_name, last_name = @last_name, birth_date = @birth_date 
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("first_name", actor.FirstName);
                    cmd.Parameters.AddWithValue("last_name", actor.LastName);
                    cmd.Parameters.AddWithValue("birth_date", actor.BirthDate);
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<Actor> GetWithPagination(uint page, uint limit)
        {
            uint start = (page * limit) - limit;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM actors OFFSET @start LIMIT @limit";
                    cmd.Parameters.AddWithValue("start", (int)start);
                    cmd.Parameters.AddWithValue("limit", (int)limit);

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Actor> actors = new List<Actor>();
                        while (reader.Read())
                        {
                            Actor actor = new Actor(reader);
                            actors.Add(actor);
                        }

                        return actors;
                    }
                }
            }
        }

        #endregion

        public static void AddFilm(long actor_id, long film_id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO film_actor (film_id, actor_id) VALUES (@film_id, @actor_id)";
                    cmd.Parameters.AddWithValue("film_id", film_id);
                    cmd.Parameters.AddWithValue("actor_id", actor_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Film> GetAllFilms(long actor_id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT fa.film_id, fl.* FROM film_actor AS fa INNER JOIN films AS fl ON fa.film_id = fl.id WHERE fa.actor_id = @actor_id; ";
                    cmd.Parameters.AddWithValue("actor_id", actor_id);

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

        public static bool DeleteFilm(long actor_id, long film_id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM film_actor WHERE film_id = @film_id AND actor_id = @actor_id";
                    cmd.Parameters.AddWithValue("film_id", film_id);
                    cmd.Parameters.AddWithValue("actor_id", actor_id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
