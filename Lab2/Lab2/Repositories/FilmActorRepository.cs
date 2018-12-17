using Lab2.Context;
using Lab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Repositories
{
    public class FilmActorRepository
    {
        private EFContext db;

        public FilmActorRepository(EFContext context)
        {
            db = context ?? throw new ArgumentNullException("Сontext must not be null.");
        }

        public FilmActor Create(FilmActor filmActor)
        {
            if (filmActor == null)
                throw new ArgumentNullException("Film must not be null");

            return db.FilmActor.Add(filmActor);
        }

        public FilmActor Delete(int film_id, int actor_id)
        {
            if (film_id <= 0)
                throw new ArgumentException("Incorrect film id.");

            if (actor_id <= 0)
                throw new ArgumentException("Incorrect film id.");

            FilmActor filmActor = db.FilmActor.FirstOrDefault(x => x.FilmId == film_id && x.ActorId == actor_id);

            if (filmActor == null) return null;

            return db.FilmActor.Remove(filmActor);
        }

        public IEnumerable<FilmActor> Find(Func<FilmActor, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate must not be null.");

            return db.FilmActor.Where(predicate).ToList();
        }

        public IEnumerable<FilmActor> GetAll()
        {
            return db.FilmActor;
        }
    }
}
