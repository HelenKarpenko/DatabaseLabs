using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Lab1.Models;

namespace Lab1
{
    class DBManager
    {
        //private static string connString = "Server=127.0.0.1;Port=5432;Database=Test;User Id=postgres;Password=8590;";

        //public static void Insert(string name)
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = "INSERT INTO public.\"People\"(name) VALUES(@NAME)";
        //            cmd.Parameters.AddWithValue("NAME", name);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public static void Select()
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand("SELECT * FROM PUBLIC.\"People\"", conn))
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Console.WriteLine(reader.GetString(0));
        //            }
        //        }
        //    }
        //}

        //public static void Delete()
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = "DELETE FROM public.\"People\" WHERE name = @NAME";
        //            cmd.Parameters.AddWithValue("NAME", "Lena");
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public static void InsertFilm(Film film)
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = "INSERT INTO public.\"Films\"(id, title, year, genre) VALUES(@id, @title, @year, @genre)";
        //            cmd.Parameters.AddWithValue("id", film.Id);
        //            cmd.Parameters.AddWithValue("title", film.Title);
        //            cmd.Parameters.AddWithValue("year", film.Year);
        //            cmd.Parameters.AddWithValue("genre", film.Genre);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }

        //    foreach (Actor actor in film.Actors)
        //    {
        //        InsertActor(actor);
        //        InsertFilmActor(film.Id, actor.Id);
        //    }

        //}

        //public static void InsertActor(Actor actor)
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = "INSERT INTO public.\"Actors\" VALUES(@id, @firstname, @lastname)";
        //            cmd.Parameters.AddWithValue("id", actor.Id);
        //            cmd.Parameters.AddWithValue("firstname", actor.FirstName);
        //            cmd.Parameters.AddWithValue("lastname", actor.LastName);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //private static void InsertFilmActor(int filmId, int actorId)
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = "INSERT INTO public.\"Films_Actors\" VALUES(@filmId, @actorId)";
        //            cmd.Parameters.AddWithValue("filmId", filmId);
        //            cmd.Parameters.AddWithValue("actorId", actorId);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public static void DeleteFilm(int id)
        //{
        //    using (var conn = new NpgsqlConnection(connString))
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = "DELETE FROM public.\"Films\" WHERE id = @id";
        //            cmd.Parameters.AddWithValue("id", id);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
