using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Models;

namespace TunifyPlatform.Data
{
    public class TunifyPlatformDbContext : DbContext
    {
        public TunifyPlatformDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Album> Album { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<PlaylistSongs> PlaylistSongs { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<User> User { get; set; }
    }
}
