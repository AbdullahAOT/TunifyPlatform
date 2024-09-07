using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            _context.Artist.Add(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task DeleteArtist(int id)
        {
            var existingArtist = await GetArtistById(id);
            if (existingArtist == null)
            {
                throw new InvalidOperationException("Artist not found");
            }
            _context.Artist.Remove(existingArtist);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Artist>> GetAllArtists()
        {
            return await _context.Artist.ToListAsync();
        }

        public async Task<Artist> GetArtistById(int id)
        {
            return await _context.Artist.FindAsync(id);
        }

        public async Task<Artist> UpdateArtist(int id, Artist artist)
        {
            var existingArtist = await _context.Artist.FindAsync(id);
            if (existingArtist == null)
            {
                return null;
            }

            existingArtist.Name = artist.Name;

            _context.Entry(existingArtist).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingArtist;
        }

        public async Task AddSongToArtist(int artistId, int songId)
        {
            var artist = await _context.Artist.Include(a => a.Songs).FirstOrDefaultAsync(a => a.Id == artistId);
            var song = await _context.Song.FindAsync(songId);

            if (artist == null || song == null)
            {
                throw new InvalidOperationException("Artist or Song not found");
            }

            artist.Songs.Add(song);
            await _context.SaveChangesAsync();
        }
    }
}
