namespace NZwalks.API.Models.Domain
{
    public class User_Role
    {
        public Guid Id { get; set; }    
        public Guid UserId { get; set; } 
        public User User { get; set; }
        public Role Role { get; set; }
        public object RoleId { get; internal set; }
    }
}
