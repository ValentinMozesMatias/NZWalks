using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.Domain
{
    public class DataMembers
    {
        [Required]
        [MaxLength(60)]
        [Column("Account Number", Order = 1)]
        public string NameOfDepositor { get; set; }
        public Guid Id { get; set; }
        public int AccBalance { get; set; }
    }
}
