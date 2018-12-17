using Lab2.Context;
using Lab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Repositories
{
    public class ActorRepository
    {
        private EFContext db;

        public ActorRepository(EFContext context)
        {
            db = context ?? throw new ArgumentNullException("Сontext must not be null.");
        }

        public Actor Create(Actor actor)
        {
            if (actor == null)
                throw new ArgumentNullException("Actor must not be null");

            return db.Actors.Add(actor);
        }

        public Actor Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect actor id.");

            Actor actors = db.Actors.Find(id);

            if (actors == null) return null;

            return db.Actors.Remove(actors);
        }

        public IEnumerable<Actor> Find(Func<Actor, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate must not be null.");

            return db.Actors.Where(predicate).ToList();
        }

        public Actor Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("incorrect actor id.");

            return db.Actors.Find(id);
        }

        public IEnumerable<Actor> GetAll()
        {
            return db.Actors;
        }

        public Actor Update(int id, Actor item)
        {
            if (item == null)
                throw new ArgumentNullException("Item must not be null.");

            if (id <= 0)
                throw new ArgumentException("incorrect answer id ");

            Actor actors = db.Actors.FirstOrDefault(x => x.Id == item.Id);
            if (actors == null) return null;

            db.Entry(actors).CurrentValues.SetValues(item);

            return actors;
        }
    }
}
