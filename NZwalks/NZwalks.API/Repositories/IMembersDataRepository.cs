using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Repositories
{
    public interface IMembersDataRepository
    {
        Task<List<DataMembers>> GetAllAsync();

        //Task<DataMembers> GetAsync(int accNumber);

        Task <DataMembers>AddAsync(DataMembers dataMembers);
        Task <DataMembers>GetAllAsync(Guid id);
        Task<DataMembers> UpdateAsync(Guid id, DataMembers dataMembers);
        Task<DataMembers> WidthdrawDataMemberRequestAsync(Guid id, int amount);
        Task<DataMembers> DepositDataMemberRequestAsync(Guid id, int amount);
    }
}
