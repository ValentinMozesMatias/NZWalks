using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<DomainRegion>>GetAllAsync();
        //Async Methods will be assigned according to Project logic?
        Task <DomainRegion>GetAsync(Guid id);

        Task<DomainRegion>AddAsync(DomainRegion region);

        Task<DomainRegion> DeleteAsync(Guid id);

        Task<DomainRegion> UpdateAsync(Guid id, DomainRegion region);
    }
}
