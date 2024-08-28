using Microsoft.AspNetCore.Mvc.ModelBinding;
using TunifyPlatform.Models;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IArtists
    {
        Task<Artist> CreateArtist(Artist artist);
        Task<Artist> UpdateArtist(int id, Artist artist);
        Task DeleteArtist(int id);
        Task<List<Artist>> GetAllArtists();
        Task<Artist> GetArtistById(int id);
    }
}
