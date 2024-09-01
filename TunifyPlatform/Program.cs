using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Repositories.Interfaces;
using TunifyPlatform.Repositories.Services;

namespace TunifyPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure database context
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<TunifyPlatformDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register repository services
            builder.Services.AddScoped<IArtists, ArtistService>();
            builder.Services.AddScoped<IPlaylists, PlaylistService>();
            builder.Services.AddScoped<ISongs, SongService>();
            builder.Services.AddScoped<IUsers, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapControllers();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
