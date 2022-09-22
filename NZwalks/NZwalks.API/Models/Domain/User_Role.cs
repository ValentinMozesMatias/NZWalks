using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.Domain
{
    public class User_Role
    {
        [Key]
        public Guid Id { get; set; }    
        public Guid UserId { get; set; } 
        public User User { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; internal set; }
    }
}
