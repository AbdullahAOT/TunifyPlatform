using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class PlaylistService : IPlaylists
    {
        private readonly TunifyPlatformDbContext _context;

        public PlaylistService(TunifyPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<Playlist> CreatePlaylist(Playlist playlist)
        {
            _context.Playlist.Add(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        public async Task<Playlist> UpdatePlaylist(int id, Playlist playlist)
        {
            var existingPlaylist = await _context.Playlist.FindAsync(id);
            if (existingPlaylist == null)
            {
                return null;
            }

            existingPlaylist.Name = playlist.Name;
            existingPlaylist.UserId = playlist.UserId;

            _context.Entry(existingPlaylist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingPlaylist;
        }

        public async Task DeletePlaylist(int id)
        {
            var playlist = await _context.Playlist.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlist.Remove(playlist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Playlist>> GetAllPlaylists()
        {
            return await _context.Playlist.ToListAsync();
        }

        public async Task<Playlist> GetPlaylistById(int id)
        {
            return await _context.Playlist.FindAsync(id);
        }
    }
}
