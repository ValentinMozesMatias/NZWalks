using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class LocalTestDbContext: DbContext
    {
        public LocalTestDbContext(DbContextOptions<LocalTestDbContext> options): base(options)
        {
            
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);
        }


        public DbSet<DomainRegion> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
    }
}
