using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZWalks.API.Data;

namespace NZwalks.API.Repositories
{
    public class MembersDataRepository : IMembersDataRepository
    {
        private readonly LocalTestDbContext _dbContext;

        public MembersDataRepository(LocalTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DataMembers>> GetAllAsync()
        {

            return await _dbContext.DataMembers.ToListAsync();
        }

        public async Task<DataMembers> GetAllAsync(Guid id)
        {
            return await _dbContext.DataMembers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DataMembers> AddSync(DataMembers assignValuesRequest)
        {

            await _dbContext.AddAsync(assignValuesRequest);
            await _dbContext.SaveChangesAsync();
            return (assignValuesRequest);

        }

        public async Task<DataMembers> AddAsync(DataMembers dataMembers)
        {
            //Aici a mai adaut o schimbare
            await _dbContext.DataMembers.AddAsync(dataMembers);
            await _dbContext.SaveChangesAsync();
            return (dataMembers);
        }

        public async Task<DataMembers> UpdateAsync(Guid id, DataMembers dataMembers)
        {
            var existingDataMembers = await _dbContext.DataMembers.FirstOrDefaultAsync(x => x.Id == id);


            //Here instead of null just place bad request message;
            if (dataMembers == null)
            {
                return null;
            }

            existingDataMembers.NameOfDepositor = dataMembers.NameOfDepositor;
            existingDataMembers.AccBalance = dataMembers.AccBalance;

            await _dbContext.SaveChangesAsync();
            return(existingDataMembers);
        }

        public async Task<DataMembers> WidthdrawDataMemberRequestAsync(Guid id, int amount)
        {
            var existingDataMembers = await _dbContext.DataMembers.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDataMembers == null)
            {
                throw new ArgumentException("The id entered is incorrect");
            }
            var initialBalance = existingDataMembers.AccBalance;

            if (initialBalance <= 0)
            {
                throw new ArgumentOutOfRangeException("The initial balance value must be higher than 0");
            }
            if (amount <= 0 || amount > initialBalance)
            {
                throw new ArgumentException("The amount inserted is inccorect");
            }
            initialBalance -= amount;

            existingDataMembers.AccBalance = initialBalance;
            _dbContext.DataMembers.Update(existingDataMembers);
            await _dbContext.SaveChangesAsync();

            return existingDataMembers;
        }

        public async Task<DataMembers> DepositDataMemberRequestAsync(Guid id, int amount)
        {
            var existingDataMembers = await _dbContext.DataMembers.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDataMembers == null)
            {
                throw new ArgumentException("The id entered is incorrect");
            }
            var initialBalance = existingDataMembers.AccBalance;

            if (amount <= 0)
            {
                throw new ArgumentException("The amount inserted is inccorect");
            }
            initialBalance += amount;

            existingDataMembers.AccBalance = initialBalance;
            _dbContext.DataMembers.Update(existingDataMembers);
            await _dbContext.SaveChangesAsync();

            return existingDataMembers;
        }
    }
}
