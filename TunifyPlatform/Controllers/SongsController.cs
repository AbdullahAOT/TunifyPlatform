using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongs _songService;

        public SongsController(ISongs songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return Ok(await _songService.GetAllSongs());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _songService.GetSongById(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            var createdSong = await _songService.CreateSong(song);
            return CreatedAtAction(nameof(GetSong), new { id = createdSong.Id }, createdSong);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            var updatedSong = await _songService.UpdateSong(id, song);
            if (updatedSong == null)
            {
                return NotFound();
            }
            return Ok(updatedSong);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            await _songService.DeleteSong(id);
            return NoContent();
        }
    }
}

