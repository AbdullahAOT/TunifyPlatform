namespace TunifyPlatform.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
