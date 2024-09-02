using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;  // Import this namespace
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

            // Add Swagger services
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tunify API",
                    Version = "v1",
                    Description = "API for managing playlists, songs, and artists in the Tunify Platform"
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tunify API v1");
                options.RoutePrefix = ""; // Set to an empty string to serve Swagger UI at the app's root
            });

            app.UseAuthorization();

            app.MapControllers();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
