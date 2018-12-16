using System.Collections.Generic;
using System.Configuration;
using Lab1.Models;
using Npgsql;

namespace Lab1.Controllers
{
    class GenreController
    {
        private static string connString = ConfigurationManager.ConnectionStrings["SampleDB"].ConnectionString;

        #region CRUD

        public static void Add(Genre genre)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO genres (name, description) 
                                        VALUES (@name, @description)";
                    cmd.Parameters.AddWithValue("name", genre.Name);
                    cmd.Parameters.AddWithValue("description", genre.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Genre> GetAll()
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(@"SELECT * FROM genres", conn))
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

        public static Genre GetById(long id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM genres WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Genre genre = null;

                        while (reader.Read())
                        {
                            genre = new Genre(reader);
                        }

                        return genre;
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
                    cmd.CommandText = @"DELETE FROM genres WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool Update(long id, Genre genre)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE genres 
                                        SET name = @name, 
                                            description = @description 
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("name", genre.Name);
                    cmd.Parameters.AddWithValue("description", genre.Description);
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static List<Genre> GetWithPagination(uint page, uint limit)
        {
            uint start = (page * limit) - limit;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM genres OFFSET @start LIMIT @limit";
                    cmd.Parameters.AddWithValue("start", (int)start);
                    cmd.Parameters.AddWithValue("limit", (int)limit);

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

        #endregion

        //public static List<Film> GetByTitle(string str)
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = "SELECT * FROM films WHERE title LIKE '" + @str + "%'";
        //            cmd.Parameters.AddWithValue("str", str);
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                List<Film> films = new List<Film>();
        //                while (reader.Read())
        //                {
        //                    Film film = new Film(reader);
        //                    films.Add(film);
        //                }

        //                return films;
        //            }
        //        }
        //    }
        //}





    }
}
