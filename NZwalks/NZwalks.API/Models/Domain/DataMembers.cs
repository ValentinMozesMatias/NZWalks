using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZwalks.API.Models.Domain
{
    public class DataMembers
    {
        //Data Properties
        [Required]
        [MaxLength(60)]
        [Column("Account Number", Order = 2)]
        public string NameOfDepositor { get; set; }
        public Guid Id { get; set; }
        public int AccBalance { get; set; }
    }
}
