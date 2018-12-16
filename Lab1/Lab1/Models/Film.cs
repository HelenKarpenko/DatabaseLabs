using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Lab1.Printer;
using System.Reflection;

namespace Lab1.Models
{
    public class Film : IPrintable
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public bool IsReleased { get; set; }
        public int Rating { get; set; }

        public Film(long id, 
                    string title,
                    int year, 
                    string description,
                    int rating,
                    bool isReleased = false)
        {
            Id = id;
            Title = title;
            Year = year;
            Description = description;
            Rating = rating;
            IsReleased = isReleased;
        }

        public Film()
        {
        }

        public static Film GetRandomFilm()
        {
            Random random = new Random();

            Film film = new Film();
            film.Title = Faker.Company.Name();
            film.Year = random.Next(1900, 2020);
            film.Description = Faker.Lorem.Paragraph();
            film.IsReleased = true;
            //film.IsReleased = random.Next(100) % 2 == 0;
            film.Rating = film.IsReleased ? random.Next(0, 100) : 0;

            return film;
        }

        public Film(NpgsqlDataReader reader)
        {
            Id = (int)reader["id"];
            Title = (string)reader["title"];
            var y = (int)reader["year"];
            Year = (int)reader["year"];
            Description = (string)reader["description"];
            IsReleased = (bool)reader["is_released"];
            Rating = (int)reader["rating"];
        }


        public List<string> GetAllFieldsValues()
        {
            List<string> values = new List<string>();

            values.Add(Id.ToString());
            values.Add(Title);
            values.Add(Year.ToString());
            values.Add(Description);
            values.Add(IsReleased.ToString());
            values.Add(Rating.ToString());

            return values;
        }

        public List<string> GetAllFieldsNames()
        {
            List<string> fields = new List<string>();

            fields.Add("id");
            fields.Add("Title");
            fields.Add("Year");
            fields.Add("Description");
            fields.Add("IsReleased");
            fields.Add("Rating");

            return fields;
        }

        public List<int> GetAllFieldsWidth()
        {
            List<int> width = new List<int>();
            width.Add(5);
            width.Add(25);
            width.Add(6);
            width.Add(140);
            width.Add(10);
            width.Add(6);
            return width;
        }
    }
}
