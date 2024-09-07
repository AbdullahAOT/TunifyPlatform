using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylists _playlistService;

        public PlaylistsController(IPlaylists playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return Ok(await _playlistService.GetAllPlaylists());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _playlistService.GetPlaylistById(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist);
        }

        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            var createdPlaylist = await _playlistService.CreatePlaylist(playlist);
            return CreatedAtAction(nameof(GetPlaylist), new { id = createdPlaylist.Id }, createdPlaylist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(int id, Playlist playlist)
        {
            var updatedPlaylist = await _playlistService.UpdatePlaylist(id, playlist);
            if (updatedPlaylist == null)
            {
                return NotFound();
            }
            return Ok(updatedPlaylist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistService.DeletePlaylist(id);
            return NoContent();
        }

        [HttpPost("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, int songId)
        {
            try
            {
                await _playlistService.AddSongToPlaylist(playlistId, songId);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("{playlistId}/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsForPlaylist(int playlistId)
        {
            var playlist = await _playlistService.GetPlaylistById(playlistId);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist);
        }
    }
}
