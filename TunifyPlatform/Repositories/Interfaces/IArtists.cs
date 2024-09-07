using TunifyPlatform.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IArtists
    {
        Task<Artist> CreateArtist(Artist artist);
        Task<Artist> UpdateArtist(int id, Artist artist);
        Task DeleteArtist(int id);
        Task<List<Artist>> GetAllArtists();
        Task<Artist> GetArtistById(int id);
        Task AddSongToArtist(int artistId, int songId);
    }
}
