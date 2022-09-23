using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class AssignValuesRequest
    {
        [Required]
        [MaxLength(60)]
        public string NameOfDepositor { get; set; }
        public Guid Id { get; set; }
        public int AccBalance { get; set; }
    }
}
