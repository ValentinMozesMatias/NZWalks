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

        public async Task<DomainRegion> AddAsync(DomainRegion region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<DomainRegion> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
            if(region == null) return null;

            //Delete region

            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<DomainRegion>>GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<DomainRegion> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<DomainRegion> UpdateAsync(Guid id, DomainRegion region)
        {
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nZWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
