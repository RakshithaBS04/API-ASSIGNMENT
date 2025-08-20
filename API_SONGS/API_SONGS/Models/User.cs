namespace API_SONGS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; } 
        public string? Role { get; set; } 

        public ICollection<Playlist>? Playlists { get; set; }
    }
}
