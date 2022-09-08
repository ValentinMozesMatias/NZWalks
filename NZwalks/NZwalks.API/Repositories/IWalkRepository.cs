using NZwalks.API.Models.DTO;
using NZWalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();

       Task<Walk> GetAsync(Guid id);

        //We will return the walk that was created. I don't understand why we don't use here the id to return the walk that was created

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> UpdateAsync(Guid id, Walk walkDomain);

        Task<Walk> DeleteAsync(Guid id);
    }
}
