using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class LocalTestDbContext: DbContext
    {
        public LocalTestDbContext(DbContextOptions<LocalTestDbContext> options): base(options)
        {
            
    }
        public DbSet<DomainRegion> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
