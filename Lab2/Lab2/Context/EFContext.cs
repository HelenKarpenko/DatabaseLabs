using Lab2.Entities;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Lab2.Context
{
    public class EFContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<FilmGenre> FilmGenre { get; set; }
        public DbSet<FilmActor> FilmActor { get; set; }

        public EFContext() : base(nameOrConnectionString: "SampleDB") { }
        
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            Database.SetInitializer<EFContext>(new DbInitializer());
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);

            builder.Entity<FilmGenre>()
                .HasKey(fg => new { fg.FilmId, fg.GenreId });

            builder.Entity<FilmGenre>()
                .HasRequired(fg => fg.Film)
                .WithMany(f => f.FilmGenre)
                .HasForeignKey(fg => fg.FilmId);

            builder.Entity<FilmGenre>()
                .HasRequired(fg => fg.Genre)
                .WithMany(g => g.FilmGenre)
                .HasForeignKey(fg => fg.GenreId);


            builder.Entity<FilmActor>()
                .HasKey(fg => new { fg.FilmId, fg.ActorId });

            builder.Entity<FilmActor>()
                .HasRequired(fg => fg.Film)
                .WithMany(f => f.FilmActor)
                .HasForeignKey(fg => fg.FilmId);

            builder.Entity<FilmActor>()
                .HasRequired(fg => fg.Actor)
                .WithMany(a => a.FilmActor)
                .HasForeignKey(fg => fg.ActorId);
        }
    }


    public class DbInitializer : DropCreateDatabaseIfModelChanges<EFContext>
    {
        protected override void Seed(EFContext db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            SetFilms(db);
            SetGenre(db);
            SetFilmGenre(db);
            SetActors(db);
            SetFilmActor(db);
        }

        private void SetFilms(EFContext db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            db.Films.Add(Film.GetRandom());
            db.Films.Add(Film.GetRandom());
            db.Films.Add(Film.GetRandom());
            db.Films.Add(Film.GetRandom());
            db.Films.Add(Film.GetRandom());
            db.Films.Add(Film.GetRandom());

            db.SaveChanges();
        }

        private void SetGenre(EFContext db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            db.Genres.Add(new Genre { Name = "A", Description = "A" });
            db.Genres.Add(new Genre { Name = "B", Description = "A" });
            db.Genres.Add(new Genre { Name = "C", Description = "A" });
            db.Genres.Add(new Genre { Name = "D", Description = "A" });
            db.Genres.Add(new Genre { Name = "E", Description = "A" });

            db.SaveChanges();
        }

        private void SetFilmGenre(EFContext db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            db.FilmGenre.Add(new FilmGenre
            {
                FilmId = 3,
                Film = db.Films.Find(3),
                GenreId = 2,
                Genre = db.Genres.Find(2),
            });
            db.FilmGenre.Add(new FilmGenre
            {
                FilmId = 4,
                Film = db.Films.Find(4),
                GenreId = 3,
                Genre = db.Genres.Find(3),
            });
            db.FilmGenre.Add(new FilmGenre
            {
                FilmId = 5,
                Film = db.Films.Find(5),
                GenreId = 3,
                Genre = db.Genres.Find(3),
            });

            db.SaveChanges();
        }

        private void SetActors(EFContext db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            db.Actors.Add(Actor.GetRandom());
            db.Actors.Add(Actor.GetRandom());
            db.Actors.Add(Actor.GetRandom());
            db.Actors.Add(Actor.GetRandom());
            db.Actors.Add(Actor.GetRandom());
            db.Actors.Add(Actor.GetRandom());
            db.Actors.Add(Actor.GetRandom());

            db.SaveChanges();
        }

        private void SetFilmActor(EFContext db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            db.FilmActor.Add(new FilmActor
            {
                FilmId = 3,
                Film = db.Films.Find(3),
                ActorId = 1,
                Actor = db.Actors.Find(1),
            });
            db.FilmActor.Add(new FilmActor
            {
                FilmId = 3,
                Film = db.Films.Find(3),
                ActorId = 2,
                Actor = db.Actors.Find(2),
            });
            db.FilmActor.Add(new FilmActor
            {
                FilmId = 3,
                Film = db.Films.Find(3),
                ActorId = 3,
                Actor = db.Actors.Find(3),
            });

            db.FilmActor.Add(new FilmActor
            {
                FilmId = 4,
                Film = db.Films.Find(4),
                ActorId = 2,
                Actor = db.Actors.Find(2),
            });
            db.FilmActor.Add(new FilmActor
            {
                FilmId = 5,
                Film = db.Films.Find(5),
                ActorId = 2,
                Actor = db.Actors.Find(2),
            });

            db.SaveChanges();
        }
    }
}

