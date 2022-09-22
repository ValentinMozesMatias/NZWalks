using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class WidthdrawDataMemberRequest
    {
        class WidthdrawAccount
        {
            [Required]
            [MaxLength(60)]
            public string NameOfDepositor { get; set; }
            public int CurrentBalance { get; set; }
            public WidthdrawAccount(int initialBalance)
            {
                if (initialBalance < 0)
                {
                    throw new ArgumentOutOfRangeException("The initial balance value must be higher than 0");
                    this.CurrentBalance = initialBalance;      
                }
            }
            public bool Deposit(int amount)
            {
                if(amount < 0)
                {
                    return false;
                }
                this.CurrentBalance += amount;
                return true;
            }
            public bool Widthdraw(int amount)
            {
                if(amount <= 0 || amount > this.CurrentBalance) 
                { 
                    return false; 
                }
                this.CurrentBalance -= amount;
                return true;
            }
        }

    }
}
