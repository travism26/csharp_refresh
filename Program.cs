using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{

    class Person
    {
        String name;
        int age;

        public Person(String name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public String getName()
        {
            return this.name;
        }
    }


    /*
     * Lets now try some advanced stuff homie polymorphism banking example? 
     * maybe try event sourcing simulate bank ledger or banking system?.
     */


    // whats the domain? or nouns: ATMS, Bank cards (interface with ATMS), Bank (holding them gold bars), 
    // Accounts (Chequing / Savings? abstract class? interface? use polymorphish here)
    // Transactions -> interface with accounts 

    interface IAccountType
    {
        double getBalance();
        double credit(int amount);
        double debit(int amount);
    }

    class ChequingAccount : IAccountType
    {

        double balance = 0;
        bool accountLocked = true;
        Dictionary<int, double> transactionHistory = new Dictionary<int, double>();
        public ChequingAccount(double accountBalance, bool accountLocked)
        {
            this.balance = accountBalance;
            this.accountLocked = accountLocked;
        }
        // Some user object that `OWNS` the `ACCOUNT` we need to create the use class.
        // This is a BANK account
        public double credit(int amount)
        {
            Console.WriteLine("We are attempting to credit the account with:"+ amount);
            updateTransactionHistory(amount); // we can add error checking roll back transaction here with a bool swap void->bool
            this.balance += amount;
            return this.balance;
        }

        public double debit(int amount)
        {
            Console.WriteLine("We are attempting to debit the account with:"+ amount);
            double debitValue = amount * -1; // simple way to get a negative value 
            updateTransactionHistory(debitValue); // we can add error checking roll back transaction here with a bool swap void->bool
            this.balance += debitValue;
            return this.balance;
        }

        public double getBalance()
        {
            return this.balance;
        }

        // keep track of that transaction history.
        private void updateTransactionHistory(double amount)
        {
            int transactionNumber = getNextID(this.transactionHistory);
            this.transactionHistory.Add(transactionNumber, amount);
        }

        public int getNextID(Dictionary<int, double> input)
        {
            if (input.Count() == 0)
            {
                return 1;
            }
            int nextBigID = input.Keys.Max() + 1;
            return nextBigID;
        }
    }

    class BigBadBank
    {
        static void Main(string[] args)
        {
            // our user repo this can be a DB or something like that impl the repo service.
            IUserRepo userRepo = new InMemoryUserDB();
            IAccountDBRepo accountDBRepo = new InMemoryAccountRepo();

            // Create a user for the bank
            Person travis = new Person("Travis", 22);
            userRepo.CreateUser(travis);

            // create a the chequing account outside creation method.
            IAccountType someoneElsesAccount = new ChequingAccount(1000, false);
            Person tom = new Person("Tommy", 99);
            IAccountOwner tomsAccount = new PersonalAccount(tom, someoneElsesAccount);

            // create a new Personal Bank Account and attach `travis` to the bank account with 100 bucks.
            IAccountOwner TravisAccount = new PersonalAccount(travis, new ChequingAccount(100.5, false));

            double travsBalance = TravisAccount.GetAccount().credit(50);
            Console.WriteLine("TravisAccount Balance:" + travsBalance);

            // flaw in design... only owner of the account should be allow to access their account. FIXED
            // double getSomeoneElseAccountBalance = TravisAccount.getAccountBalance(someoneElsesAccount);
            // Console.WriteLine("someone else account balance using TravisAccount object:" + getSomeoneElseAccountBalance + "$ <--- flaw / bug");


            // lets create a `List` of account holders and run reporting methods off them using polymorphism
            List<IAccountOwner> allAccountOwnerList = new List<IAccountOwner>();
            allAccountOwnerList.Add(tomsAccount);
            allAccountOwnerList.Add(TravisAccount);
            double totalBalance =0;
            foreach(PersonalAccount owner in allAccountOwnerList){
                totalBalance += owner.getAccountBalance();
            }
            Console.WriteLine("Total stored by bank:" + totalBalance);
        }
    }
}
