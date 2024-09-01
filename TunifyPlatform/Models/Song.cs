namespace TunifyPlatform.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }

        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
        public ICollection<Artist> Artists { get; set; } = new List<Artist>();
    }
}
