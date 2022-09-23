using NZwalks.API.Migrations;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace NZwalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly LocalTestDbContext nZWalksDbContext;

        public WalkDifficultyRepository(LocalTestDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var deleteWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if (deleteWalkDifficulty == null)
            {
                return null;
            }

            nZWalksDbContext.WalkDifficulty.Remove(deleteWalkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();

            return deleteWalkDifficulty;


        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            {
                if (walkDifficulty == null)
                {
                    return null;
                }

                existingWalkDifficulty.Code = walkDifficulty.Code;
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalkDifficulty;
            }
        }

    }
    
}
