using NZWalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        //Here we create the CRUD interface which are GetAll, GetById , Update && Delete;
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();

        //Get Method by Id
        Task<WalkDifficulty> GetAsync(Guid id);

        //Add Method

        Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);
        //Update

        Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty);
        //Delete
        Task<WalkDifficulty> DeleteAsync(Guid id);
        


            }
}
