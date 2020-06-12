using Microsoft.EntityFrameworkCore;
using UwpCommunity.Data.Models;

namespace UwpCommunity.Data
{
    public class UwpCommunityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }


        public UwpCommunityDbContext() { }
        public UwpCommunityDbContext(DbContextOptions<UwpCommunityDbContext> options) : base(options) { }
    }
}
