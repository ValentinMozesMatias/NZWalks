using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;
using NZWalks.API.Data;

namespace NZwalks.API.Repositories
{
    public class MembersDataRepository : IMembersDataRepository
    {
        public MembersDataRepository(LocalTestDbContext nZWalksDbContext)
        {
            this.NZWalksDbContext = nZWalksDbContext;
        }

        public LocalTestDbContext NZWalksDbContext { get; set; }

        public async Task<IEnumerable<DataMembers>> GetAllAsync()
        {

            return await NZWalksDbContext.DataMembers.ToListAsync();
        }

        public async Task<DataMembers> GetAsync(int AccNumber)
        {
            return await NZWalksDbContext.DataMembers.FirstOrDefaultAsync(x => x.AccNumber == AccNumber);
             
        }
    }
}
