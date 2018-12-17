using Lab2.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab2.Entities
{
    public class Genre : IPrintable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<FilmGenre> FilmGenre { get; set; }

        public Genre()
        {
            FilmGenre = new HashSet<FilmGenre>();
        }

        public static Genre GetRandom()
        {
            Random random = new Random();

            Genre genre = new Genre
            {
                Name = Faker.Company.Name(),
                Description = Faker.Lorem.Paragraph(),
            };
            return genre;
        }

        public List<string> GetAllFieldsNames()
        {
            List<string> fields = new List<string>
            {
                "id",
                "Name",
                "Description"
            };

            return fields;
        }

        public List<int> GetAllFieldsWidth()
        {
            List<int> width = new List<int>
            {
                5,
                25,
                140
            };
            return width;
        }

        public List<string> GetAllFieldsValues()
        {
            List<string> values = new List<string>
            {
                Id.ToString(),
                Name,
                Description
            };

            return values;
        }
    }
}
