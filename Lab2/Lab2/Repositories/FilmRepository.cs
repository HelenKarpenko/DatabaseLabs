using Lab2.Context;
using Lab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Repositories
{
    public class FilmRepository
    {
        private EFContext db;

        public FilmRepository(EFContext context)
        {
            db = context ?? throw new ArgumentNullException("Сontext must not be null.");
        }

        public Film Create(Film film)
        {
            if (film == null)
                throw new ArgumentNullException("Film must not be null");

            return db.Films.Add(film);
        }

        public Film Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect film id.");

            Film film = db.Films.Find(id);

            if (film == null) return null;

            return db.Films.Remove(film);
        }

        public IEnumerable<Film> Find(Func<Film, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate must not be null.");

            return db.Films.Where(predicate).ToList();
        }

        public Film Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect film id.");

            return db.Films.Find(id);
        }

        public IEnumerable<Film> GetAll()
        {
            return db.Films;
        }

        public Film Update(Film item)
        {
            if (item == null)
                throw new ArgumentNullException("Item must not be null.");

            if (item.Id <= 0)
                throw new ArgumentException("incorrect answer id ");

            Film film = db.Films.FirstOrDefault(x => x.Id == item.Id);
            if (film == null) return null;

            db.Entry(film).CurrentValues.SetValues(item);

            return film;
        }
    }
}
