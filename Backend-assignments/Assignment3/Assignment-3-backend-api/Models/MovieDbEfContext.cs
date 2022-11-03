using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Assignment_3_backend_api.Models
{
    public class MovieDbEfContext : DbContext
    {

        public MovieDbEfContext(DbContextOptions options) : base(options)
        {
        }

        protected MovieDbEfContext()
        {
        }

        public virtual DbSet<Character>? Characters { get; set; } = null!;
        public virtual DbSet<Movie>? Movies { get; set; } = null!;
        public virtual DbSet<Franchise>? Franchises { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Character>().HasData(new Character { Id = 1, FullName = "Hame Ha", Alias = "Cool", Picture = "img" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 2, FullName = "James Aloy", Alias = "Nice", Picture = "img" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 3, FullName = "Plat Yo", Alias = "Cold", Picture = "img" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 4, FullName = "Sri Pol", Alias = "Angry", Picture = "img" });
            modelBuilder.Entity<Character>().HasData(new Character { Id = 5, FullName = "Wer Hi", Alias = "Smart", Picture = "img" });

            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 1, Description = "Cool franchise", Name = "The god of franchise" });
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 2, Description = "Queen of franchise", Name = "The goddess of franchise" });


            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 1,
                Director = "Momo",
                Genre = "Action, Drama",
                Picture = "img",
                ReleaseYear = 2303,
                MovieTitle = "The king returns",
                Trailer = "trail",
                FranchiseId = 1,
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 2,
                Director = "Momo",
                Genre = "Action, Drama, Romance",
                Picture = "img",
                ReleaseYear = 2343,
                MovieTitle = "The queen returns",
                Trailer = "trail",
                FranchiseId = 1,
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 3,
                Director = "Albert",
                Genre = "Action, Drama, Horror",
                Picture = "img",
                ReleaseYear = 2503,
                MovieTitle = "The dragon",
                Trailer = "trail",
                FranchiseId = 2,
            });


            modelBuilder.Entity<Movie>()
               .HasMany(c => c.Characters)
               .WithMany(m => m.Movies)
               .UsingEntity<Dictionary<string, object>>(
                   "CharacterMovie",
                   r => r.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                   l => l.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                   je =>
                   {
                       je.HasKey("CharactersId", "MoviesId");
                       je.HasData(
                           new { CharactersId = 1, MoviesId = 1 },
                           new { CharactersId = 2, MoviesId = 1 },
                           new { CharactersId = 3, MoviesId = 1 },
                           new { CharactersId = 1, MoviesId = 2 },
                           new { CharactersId = 2, MoviesId = 2 },
                           new { CharactersId = 3, MoviesId = 2 },
                           new { CharactersId = 4, MoviesId = 2 },
                           new { CharactersId = 2, MoviesId = 3 },
                           new { CharactersId = 3, MoviesId = 3 },
                           new { CharactersId = 4, MoviesId = 3 },
                           new { CharactersId = 5, MoviesId = 3 }
                       );
                   });
        }
    }
}
