using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;
using NZWalks.API.Data;

namespace NZwalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private readonly LocalTestDbContext _dbContext;
        public StaticUserRepository(LocalTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Here is does not accept a bool; can't implement IUserRepository if there is not a matching Task of type<User>
        public async Task<User> AuthenticateAsync(string username, string password)
        {

            var user = await _dbContext.Users
                .Where(x => x.Username == username && x.Password == password)
                .FirstOrDefaultAsync();
               

            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
