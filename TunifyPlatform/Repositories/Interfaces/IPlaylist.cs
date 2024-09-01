using TunifyPlatform.Models;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IPlaylists
    {
        Task<Playlist> CreatePlaylist(Playlist playlist);
        Task<Playlist> UpdatePlaylist(int id, Playlist playlist);
        Task DeletePlaylist(int id);
        Task<List<Playlist>> GetAllPlaylists();
        Task<Playlist> GetPlaylistById(int id);
        Task AddSongToPlaylist(int playlistId, int songId);
    }
}
