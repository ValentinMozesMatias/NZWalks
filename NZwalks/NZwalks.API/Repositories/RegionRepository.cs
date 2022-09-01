using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class RegionRepository: IRegionRepository
    {
        private readonly LocalTestDbContext nZWalksDbContext;

        public RegionRepository(LocalTestDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public LocalTestDbContext NZWalksDbContext { get; }

        public async Task<IEnumerable<Region>>GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }
    }
}
