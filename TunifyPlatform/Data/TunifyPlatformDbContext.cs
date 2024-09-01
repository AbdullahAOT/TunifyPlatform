using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Models;

namespace TunifyPlatform.Data
{
    public class TunifyPlatformDbContext : DbContext
    {
        public TunifyPlatformDbContext(DbContextOptions<TunifyPlatformDbContext> options) : base(options)
        {
        }

        public DbSet<Album> Album { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define many-to-many relationships
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Songs)
                .WithMany(s => s.Playlists)
                .UsingEntity(j => j.ToTable("PlaylistSongs"));

            modelBuilder.Entity<Song>()
                .HasMany(s => s.Artists)
                .WithMany(a => a.Songs)
                .UsingEntity(j => j.ToTable("ArtistSongs"));

            // Seed initial data
            modelBuilder.Entity<Playlist>().HasData(
                new Playlist { Id = 1, Name = "Chill Vibes", UserId = 1 }
            );

            modelBuilder.Entity<Song>().HasData(
                new Song { Id = 1, Title = "Sunny Day", Genre = "Pop" },
                new Song { Id = 2, Title = "Rainy Evening", Genre = "Jazz" }
            );

            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, Name = "John Doe" },
                new Artist { Id = 2, Name = "Jane Smith" }
            );
        }
    }
}
