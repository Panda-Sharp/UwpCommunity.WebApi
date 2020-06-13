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
            Map(modelBuilder.Entity<Category>());
            Map(modelBuilder.Entity<Launch>());
            Map(modelBuilder.Entity<LaunchProject>());
            Map(modelBuilder.Entity<Project>());
            Map(modelBuilder.Entity<Role>());
            Map(modelBuilder.Entity<User>());
            Map(modelBuilder.Entity<UserProject>());
        }

        private static void Map(EntityTypeBuilder<Category> entity)
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.HasIndex(x => x.LastUpdated);
        }

        private static void Map(EntityTypeBuilder<Launch> entity)
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.HasIndex(x => x.LastUpdated);
        }

        private static void Map(EntityTypeBuilder<LaunchProject> entity)
        {
            entity.HasKey(lp => new { lp.LaunchId, lp.ProjectId });

            entity.HasOne(lp => lp.Launch)
                .WithMany(p => p.LaunchProjects)
                .HasForeignKey(lp => lp.LaunchId);

            entity.HasOne(lp => lp.Project)
                .WithMany(t => t.LaunchProjects)
                .HasForeignKey(lp => lp.ProjectId);
        }

        private static void Map(EntityTypeBuilder<Project> entity)
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.HasIndex(x => x.LastUpdated);
        }

        private static void Map(EntityTypeBuilder<Role> entity)
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.HasIndex(x => x.LastUpdated);
        }

        private static void Map(EntityTypeBuilder<User> entity)
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.HasIndex(x => x.LastUpdated);
        }

        private static void Map(EntityTypeBuilder<UserProject> entity)
        {
            entity.HasKey(up => new { up.UserId, up.ProjectId });

            entity.HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);

            entity.HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);
        }
    }
}
