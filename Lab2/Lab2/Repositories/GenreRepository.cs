using Lab2.Context;
using Lab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Repositories
{
    public class GenreRepository
    {
        private EFContext db;

        public GenreRepository(EFContext context)
        {
            db = context ?? throw new ArgumentNullException("Сontext must not be null.");
        }

        public Genre Create(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException("Genre must not be null");

            return db.Genres.Add(genre);
        }

        public Genre Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect genre id.");

            Genre genre = db.Genres.Find(id);

            if (genre == null) return null;

            return db.Genres.Remove(genre);
        }

        public IEnumerable<Genre> Find(Func<Genre, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate must not be null.");

            return db.Genres.Where(predicate).ToList();
        }

        public Genre Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect genre id.");

            return db.Genres.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return db.Genres;
        }

        public Genre Update(int id, Genre item)
        {
            if (item == null)
                throw new ArgumentNullException("Item must not be null.");

            if (id <= 0)
                throw new ArgumentException("incorrect answer id ");

            Genre genre = db.Genres.FirstOrDefault(x => x.Id == item.Id);
            if (genre == null) return null;

            db.Entry(genre).CurrentValues.SetValues(item);

            return genre;
        }
    }
}
