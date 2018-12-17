using Lab2.Context;
using Lab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Repositories
{
    public class FilmGenreRepository
    {
        private EFContext db;

        public FilmGenreRepository(EFContext context)
        {
            db = context ?? throw new ArgumentNullException("Сontext must not be null.");
        }

        public FilmGenre Create(FilmGenre filmGenre)
        {
            if (filmGenre == null)
                throw new ArgumentNullException("Film must not be null");

            return db.FilmGenre.Add(filmGenre);
        }

        public FilmGenre Delete(int film_id, int genre_id)
        {
            if (film_id <= 0)
                throw new ArgumentException("Incorrect film id.");

            if (genre_id <= 0)
                throw new ArgumentException("Incorrect film id.");

            FilmGenre filmGanre = db.FilmGenre.FirstOrDefault(x => x.FilmId == film_id && x.GenreId == genre_id);

            if (filmGanre == null) return null;

            return db.FilmGenre.Remove(filmGanre);
        }

        public IEnumerable<FilmGenre> Find(Func<FilmGenre, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate must not be null.");

            return db.FilmGenre.Where(predicate).ToList();
        }
        
        public IEnumerable<FilmGenre> GetAll()
        {
            return db.FilmGenre;
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
