using Microsoft.EntityFrameworkCore;
using API_SONGS.Models;

namespace API_SONGS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Playlist> Playlists => Set<Playlist>();
    }
}
