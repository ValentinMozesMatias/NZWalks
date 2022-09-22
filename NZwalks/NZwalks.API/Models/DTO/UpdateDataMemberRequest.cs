using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class UpdateDataMemberRequest
    {

        //Ramane sa adaug doar readonly pentru string si Guid

        [Required]
        [MaxLength(60)]
        [Column("Account Number", Order = 2)]
        public string NameOfDepositor { get; set; }
        public int AccBalance { get; set; }
    }
}
