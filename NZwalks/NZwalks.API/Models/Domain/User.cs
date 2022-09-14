using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Models.Domain
{
    public class User
    {
        public Guid id { get; set; }
        public string Username { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Navigation
        public List<User_Role> UserRoles { get; set; }

    }
}
