using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class SongService : ISongs
    {
        private readonly TunifyPlatformDbContext _context;

        public SongService(TunifyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<Song> CreateSong(Song song)
        {
            _context.Song.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<Song> UpdateSong(int id, Song song)
        {
            var existingSong = await _context.Song.FindAsync(id);
            if (existingSong == null)
            {
                return null;
            }

            existingSong.Title = song.Title;
            existingSong.Genre = song.Genre;

            _context.Entry(existingSong).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingSong;
        }

        public async Task DeleteSong(int id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song != null)
            {
                _context.Song.Remove(song);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Song>> GetAllSongs()
        {
            return await _context.Song.ToListAsync();
        }

        public async Task<Song> GetSongById(int id)
        {
            return await _context.Song.FindAsync(id);
        }
    }
}
