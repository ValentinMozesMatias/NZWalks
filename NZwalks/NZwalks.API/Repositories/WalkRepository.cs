using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Migrations;
using NZwalks.API.Models.DTO;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        public WalkRepository(LocalTestDbContext nZWalksDbContext)
        {
            this.NZWalksDbContext = nZWalksDbContext;
        }

        public LocalTestDbContext NZWalksDbContext { get; }

        public async Task<Walk> AddAsync(Walk walk)
        {

            walk.Id = Guid.NewGuid();
            await NZWalksDbContext.AddAsync(walk);
            await NZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
                NZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public Task<Walk> GetAsync(Guid id)
        {
           return  NZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await NZWalksDbContext.Walks.FindAsync(id);

            if (existingWalk != null)
            {
                existingWalk.Name = walk.Name;
                existingWalk.Length = walk.Length;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                await NZWalksDbContext.SaveChangesAsync(true);
                return existingWalk;
            }

            return null;
            
        }

        public async Task<Walk>DeleteAsync(Guid id)
        {
            var walk = await NZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null) return null;

            //Delete
            
            NZWalksDbContext.Walks.Remove(walk);
            await NZWalksDbContext.SaveChangesAsync();

            return walk;

        }

    }


}
