using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName = "Read Only", LastName = "User", EmailAdress = "readonly@user.com", id = Guid.NewGuid(),
                Username = "readonly@user.com", Password = "12345", Roles = new List<string> {"reader", "writer"}
            },

            new User()
            {
                FirstName = "Read Write", LastName = "User", EmailAdress = "readwrite@user.com", id = Guid.NewGuid(),
                Username = "readwrite@user.com", Password = "12345", Roles = new List<string> {"reader", "writer"}
            }
        };
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) 
            && x.Password == password);

            return user;
        }
    }
}
