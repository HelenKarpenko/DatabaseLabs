using Lab2.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab2.Entities
{
    public class Film : IPrintable
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public bool IsReleased { get; set; }
        public int Rating { get; set; }
        public string Stars { get; set; }

        public ICollection<FilmActor> FilmActor { get; set; }
        public ICollection<FilmGenre> FilmGenre { get; set; }

        public Film()
        {
            FilmActor = new HashSet<FilmActor>();
            FilmGenre = new HashSet<FilmGenre>();
        }

        public static Film GetRandom()
        {
            Random random = new Random();

            Film film = new Film
            {
                Title = Faker.Company.Name(),
                Year = random.Next(1900, 2020),
                Description = Faker.Lorem.Paragraph(),
                IsReleased = random.Next(100) % 2 == 0,
            };
            film.Rating = film.IsReleased ? random.Next(0, 100) : 0;
            film.Stars = null;
            return film;
        }
        
        public List<string> GetAllFieldsValues()
        {
            List<string> values = new List<string>
            {
                Id.ToString(),
                Title,
                Year.ToString(),
                Description,
                IsReleased.ToString(),
                Rating.ToString(),
                Stars ?? "-",
            };

            return values;
        }

        public List<string> GetAllFieldsNames()
        {
            List<string> fields = new List<string>
            {
                "id",
                "Title",
                "Year",
                "Description",
                "IsReleased",
                "Rating",
                "Stars"
            };

            return fields;
        }

        public List<int> GetAllFieldsWidth()
        {
            List<int> width = new List<int>
            {
                5,
                25,
                6,
                140,
                10,
                6,
                7
            };
            return width;
        }
    }
}
