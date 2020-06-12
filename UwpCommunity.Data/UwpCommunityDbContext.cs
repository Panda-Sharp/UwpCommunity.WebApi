using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UwpCommunity.Data.Models;

namespace UwpCommunity.Data
{
    public class UwpCommunityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }


        public UwpCommunityDbContext() { }
        public UwpCommunityDbContext(DbContextOptions<UwpCommunityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Map(modelBuilder.Entity<User>());
            Map(modelBuilder.Entity<Project>());
            Map(modelBuilder.Entity<UserProject>());
        }

        private static void Map(EntityTypeBuilder<User> entity)
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.HasIndex(x => x.LastUpdated);
        }

        private static void Map(EntityTypeBuilder<Project> entity)
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.HasIndex(x => x.LastUpdated);
        }

        private static void Map(EntityTypeBuilder<UserProject> entity)
        {
            entity.HasKey(asa => new { asa.UserId, asa.ProjectId });

            entity.HasOne(pt => pt.User)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(pt => pt.UserId);

            entity.HasOne(pt => pt.Project)
                .WithMany(t => t.UserProjects)
                .HasForeignKey(pt => pt.ProjectId);
        }
    }
}
