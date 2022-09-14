using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IUserRepository
    {

        //De ce am transformat acest task in user din bool? 
        Task<User> AuthenticateAsync(string username, string password);
    }
}
