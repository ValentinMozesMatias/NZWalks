using System.Runtime.InteropServices;

namespace NZwalks.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(Models.Domain.User user);

    }
}
