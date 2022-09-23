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

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();

            return user;
        }
    }
}
