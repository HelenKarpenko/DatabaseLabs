using Lab2.Context;
using System;

namespace Lab2.Repositories
{
    public class UnitOfWork
    {
        private EFContext db;

        private ActorRepository actorRepository;
        private FilmRepository filmRepository;
        private GenreRepository genreRepository;

        private FilmGenreRepository filmGenreRepository;
        private FilmActorRepository filmActorRepository;

        public UnitOfWork(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("ConnectionString must not be null.");

            db = new EFContext();
        }

        public ActorRepository actors
        {
            get
            {
                if (actorRepository == null)
                    actorRepository = new ActorRepository(db);

                return actorRepository;
            }
        }

        public FilmRepository films
        {
            get
            {
                if (filmRepository == null)
                    filmRepository = new FilmRepository(db);

                return filmRepository;
            }
        }

        public GenreRepository genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);

                return genreRepository;
            }
        }

        public FilmGenreRepository FilmGenre
        {
            get
            {
                if (filmGenreRepository == null)
                    filmGenreRepository = new FilmGenreRepository(db);

                return filmGenreRepository;
            }
        }

        public FilmActorRepository FilmActor
        {
            get
            {
                if (filmActorRepository == null)
                    filmActorRepository = new FilmActorRepository(db);

                return filmActorRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region Dispose

        private bool disposed = false;

        public virtual void Dispose(bool disposed)
        {
            if (!this.disposed)
            {
                if (disposed)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
