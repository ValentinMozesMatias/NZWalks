using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class WidthdrawDataMemberRequest
    {
        [Required]
        [MaxLength(60)]
        public string NameOfDepositor { get; set; }
        public int CurrentBalance { get; set; }
        public Guid Id { get; set; }
    }
}
