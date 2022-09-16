using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IMembersDataRepository
    {
        Task<IEnumerable<DataMembers>> GetAllAsync();

        Task<DataMembers> GetAsync(int accNumber);
    }
}
