using TunifyPlatform.Models;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface ISongs
    {
        Task<Song> CreateSong(Song song);
        Task<Song> UpdateSong(int id, Song song);
        Task DeleteSong(int id);
        Task<List<Song>> GetAllSongs();
        Task<Song> GetSongById(int id);
    }
}
