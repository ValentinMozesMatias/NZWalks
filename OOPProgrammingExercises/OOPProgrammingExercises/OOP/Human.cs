using System.Security.Cryptography.X509Certificates;

namespace OOPProgrammingExercises.OOP
{
    public class BankAccount
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance { get; }
        public string CardType { get; }

        public BankAccount (string name, decimal initialBalance)
        {
            this.Owner = name;
            this.Balance = initialBalance;
        }

        public void MakeDeposit(decimal amount, DateTime, date, string, note)
        {
           
        }

        public void MakeWidthrawal(decimal amount, DateTime, date, string, note)
        {

        }
    }
}
