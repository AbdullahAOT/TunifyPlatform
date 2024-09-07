using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Models;

namespace TunifyPlatform.Data
{
    public class TunifyPlatformDbContext : IdentityDbContext<IdentityUser>
    {
        public TunifyPlatformDbContext(DbContextOptions<TunifyPlatformDbContext> options) : base(options)
        {
        }

        public DbSet<Album> Album { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }

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

            // Seed initial data for playlists, songs, and artists
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

            // Seed initial roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role-user", Name = "User", NormalizedName = "USER" }
            );

            // Seed an admin user with a password and assign the admin role
            var adminUser = new IdentityUser
            {
                Id = "admin-id",
                UserName = "admin@tunify.com",
                NormalizedUserName = "ADMIN@TUNIFY.COM",
                Email = "admin@tunify.com",
                NormalizedEmail = "ADMIN@TUNIFY.COM",
                EmailConfirmed = true,
                SecurityStamp = string.Empty
            };

            // Hash the password for the admin user
            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

            // Assign the admin role to the admin user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = "role-admin" }
            );
        }
    }
}
