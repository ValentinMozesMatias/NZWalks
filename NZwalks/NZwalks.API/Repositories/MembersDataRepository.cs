using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZWalks.API.Data;

namespace NZwalks.API.Repositories
{
    public class MembersDataRepository : IMembersDataRepository
    {
        private readonly DataMembers assignvaluerequest;


        public MembersDataRepository(LocalTestDbContext nZWalksDbContext)
        {
            this.NZWalksDbContext = nZWalksDbContext;
        }

        public LocalTestDbContext NZWalksDbContext { get; set; }

        public async Task<List<DataMembers>> GetAllAsync()
        {

            return await NZWalksDbContext.DataMembers.ToListAsync();
        }

        public async Task<DataMembers> GetAllAsync(Guid id)
        {
            return await NZWalksDbContext.DataMembers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DataMembers> AddSync(DataMembers assignValuesRequest)
        {

            await NZWalksDbContext.AddAsync(assignValuesRequest);
            await NZWalksDbContext.SaveChangesAsync();
            return (assignValuesRequest);

        }

        public async Task<DataMembers> AddAsync(DataMembers dataMembers)
        {
            //Aici a mai adaut o schimbare
            await NZWalksDbContext.DataMembers.AddAsync(dataMembers);
            await NZWalksDbContext.SaveChangesAsync();
            return (dataMembers);
        }

        public async Task<DataMembers> UpdateAsync(Guid id, DataMembers dataMembers)
        {
            var existingDataMembers = await NZWalksDbContext.DataMembers.FirstOrDefaultAsync(x => x.Id == id);


            //Here instead of null just place bad request message;
            if (dataMembers == null)
            {
                return null;
            }

            existingDataMembers.NameOfDepositor = dataMembers.NameOfDepositor;
            existingDataMembers.AccBalance = dataMembers.AccBalance;

            await NZWalksDbContext.SaveChangesAsync();
            return(existingDataMembers);
        }
    }
}
