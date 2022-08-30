namespace OOPProgrammingExercises.OOP
{
    public class RunProgram
    {
       static void Main (string[] args)
        {
            var account = new BankAccount("Robert", 20000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with account {account.Balance} that can be widrawn from {account.CardType}");
        }
    }
}
