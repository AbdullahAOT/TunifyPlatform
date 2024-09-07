using TunifyPlatform.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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
