using GuestBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestBookApp.Data
{
    public class GuestBookContext : DbContext
    {
        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
