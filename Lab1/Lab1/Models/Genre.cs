using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Printer;
using Npgsql;
using NpgsqlTypes;

namespace Lab1.Models
{
    public class Genre : IPrintable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Genre(long id,
                    string name,
                    string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Genre()
        {
        }

        public static Genre GetRandomGenre()
        {
            Random random = new Random();

            Genre genre = new Genre();
            genre.Name = Faker.Company.Name();
            genre.Description = Faker.Lorem.Paragraph();

            return genre;
        }

        public Genre(NpgsqlDataReader reader)
        {
            Id = (int)reader["id"];
            Name = (string)reader["name"];
            Description = (string)reader["description"];
        }

        public List<string> GetAllFieldsNames()
        {
            List<string> fields = new List<string>();

            fields.Add("id");
            fields.Add("Name");
            fields.Add("Description");

            return fields;
        }

        public List<int> GetAllFieldsWidth()
        {
            List<int> width = new List<int>();
            width.Add(5);
            width.Add(25);
            width.Add(140);
            return width;
        }

        public List<string> GetAllFieldsValues()
        {
            List<string> values = new List<string>();

            values.Add(Id.ToString());
            values.Add(Name);
            values.Add(Description);

            return values;
        }
        
    }
}
