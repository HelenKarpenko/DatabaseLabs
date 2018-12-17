using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lab2.Printer;

namespace Lab2.Entities
{
    public class Actor : IPrintable
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<FilmActor> FilmActor { get; set; }

        public Actor()
        {
            FilmActor = new HashSet<FilmActor>();
        }

        public static Actor GetRandom()
        {
            Random random = new Random();

            Actor actor = new Actor
            {
                FirstName = Faker.Company.Name(),
                LastName = Faker.Company.Name(),
                BirthDate = new DateTime()
            };

            return actor;
        }

        public List<string> GetAllFieldsValues()
        {
            List<string> values = new List<string>
            {
                Id.ToString(),
                FirstName,
                LastName,
                BirthDate.ToString()
            };

            return values;
        }

        public List<string> GetAllFieldsNames()
        {
            List<string> fields = new List<string>
            {
                "id",
                "First name",
                "Last name",
                "Birth date"
            };

            return fields;
        }

        public List<int> GetAllFieldsWidth()
        {
            List<int> width = new List<int>
            {
                5,
                40,
                40,
                15
            };
            return width;
        }
    }
}
