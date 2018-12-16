using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.Printer;
using Npgsql;

namespace Lab1.Models
{
    public class Actor : IPrintable
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public Actor() { }

        public Actor(long id, string firstName, string lastName, DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public Actor(NpgsqlDataReader reader)
        {
            Id = (long)reader["id"];
            FirstName = (string)reader["first_name"];
            LastName = (string)reader["last_name"];
            BirthDate = (DateTime)reader["birth_date"];
        }

        public static Actor GetRandomActor()
        {
            Random random = new Random();

            Actor actor = new Actor();
            actor.FirstName = Faker.Company.Name();
            actor.LastName = Faker.Company.Name();
            actor.BirthDate = new DateTime();

            return actor;
        }

        public List<string> GetAllFieldsValues()
        {
            List<string> values = new List<string>();

            values.Add(Id.ToString());
            values.Add(FirstName);
            values.Add(LastName);
            values.Add(BirthDate.ToString());

            return values;
        }

        public List<string> GetAllFieldsNames()
        {
            List<string> fields = new List<string>();

            fields.Add("id");
            fields.Add("First name");
            fields.Add("Last name");
            fields.Add("Birth date");

            return fields;
        }

        public List<int> GetAllFieldsWidth()
        {
            List<int> width = new List<int>();
            width.Add(5);
            width.Add(40);
            width.Add(40);
            width.Add(15);
            return width;
        }
    }
}
