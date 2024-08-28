using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class ArtistService : IArtists
    {
        private readonly TunifyPlatformDbContext _context;

        public ArtistService(TunifyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<Artist> CreateArtist(Artist artist)
        {
            _context.Artist.AddAsync(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task DeleteArtist(int id)
        {
            // Retrieve the artist by ID
            var existingArtist = await GetArtistById(id);

            // Check if the artist exists
            if (existingArtist == null)
            {
                throw new InvalidOperationException("Artist not found");
            }

            // Remove the artist from the context
            _context.Artist.Remove(existingArtist);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task<List<Artist>> GetAllArtists()
        {
            var allEmployees = await _context.Artist.ToListAsync();
            return allEmployees;
        }

        public async Task<Artist> GetArtistById(int id)
        {
            var _artist = await _context.Artist.FindAsync(id);
            return _artist;
        }

        public async Task<Artist> UpdateArtist(int id, Artist artist)
        {
            // Retrieve the existing artist from the database
            var existingArtist = await _context.Artist.FindAsync(id);

            // Check if the artist was found
            if (existingArtist == null)
            {
                return null; // Return null or handle the scenario where the artist is not found
            }

            // Update the existing artist's properties
            existingArtist.Name = artist.Name;

            // Mark the entity as modified
            _context.Entry(existingArtist).State = EntityState.Modified;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated artist
            return existingArtist;
        }
    }
}
