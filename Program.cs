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

    // TESTING PURPOSES IN MEMORY DB
    interface IRepository
    {
        void insert(String objectName, ITableRepo repo);
        void delete(String objectName, ITableRepo repo);
        void update(String objectName, ITableRepo repo);
    }

    // sooooo I want to save multi types of information, how about we create a interface and each
    // table orrr data point repo (users, bank account, ...etc) might be a bad idea but lets try it.
    interface ITableRepo
    {
        // we can leave empty for now
    }

    // Doing this to keep it abstract...abstration is the an ally.
    interface IAccountOwner
    {
        String getAccountOwnerName();
        double getAccountBalance(IAccountType accountType);
        IAccountType GetAccount();
    }

    class PersonalAccount : IAccountOwner
    {

        // Stringly coupled `PersonAccount + Person` meh kinda a code smell but can refactor later once working...
        // easy fix create interface maybe...donno will get back to later.
        Person accountOwner;
        IAccountType accountType;
        public PersonalAccount(Person accountOwnerIn, IAccountType accountTypeIn){
            this.accountOwner =accountOwnerIn;
            this.accountType = accountTypeIn;
        }

        public double getAccountBalance(IAccountType accountType)
        {
            return accountType.getBalance();
        }

        public string getAccountOwnerName()
        {
            return this.accountOwner.getName();
        }

        public IAccountType GetAccount(){
            return this.accountType;
        }
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
            updateTransactionHistory(amount); // we can add error checking roll back transaction here with a bool swap void->bool
            this.balance += amount;
            return this.balance;
        }

        public double debit(int amount)
        {
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

    interface IUserRepo
    {
        int CreateUser(Person user);
        bool DeleteUser(int userId);
        Person getUser(int userId);
    }

    // Add the DB stuff here
    class InMemoryUserDB : IUserRepo
    {
        Dictionary<int, Person> userDB = new Dictionary<int, Person>();

        public InMemoryUserDB() { }
        public int CreateUser(Person user)
        {
            userDB.Add(getNextID(userDB), user);
            Console.WriteLine("Created User" + user.getName());
            return userDB.Keys.Max();
        }

        public int getNextID(Dictionary<int, Person> userDB)
        {

            if (userDB.Count() == 0)
            {
                return 1;
            }
            int nextBigID = userDB.Keys.Max() + 1;
            return nextBigID;
        }

        public bool DeleteUser(int userId)
        {
            bool isRemoved = this.userDB.Remove(userId);
            return isRemoved;
        }

        public Person getUser(int userId)
        {
            return this.userDB[userId];
        }
    }

    interface IAccountDBRepo
    {
        int CreateAccount(IAccountType accountType);
        bool DeleteAccount(int accountID);
        IAccountType GetAccount(int accountID);
    }

    class InMemoryAccountRepo : IAccountDBRepo
    {

        Dictionary<int, IAccountType> accountDB = new Dictionary<int, IAccountType>();
        public int CreateAccount(IAccountType accountType)
        {
            int accountNumber = getNextID(this.accountDB);
            this.accountDB.Add(accountNumber, accountType);
            return accountNumber;
        }

        public bool DeleteAccount(int accountID)
        {
            return this.accountDB.Remove(accountID); // validation? meh maybe later once complete I know know NOT Secure coding
        }

        public IAccountType GetAccount(int accountID)
        {
            return this.accountDB[accountID]; // again no validation but ill get to later...
            // i know we can use an interface to auto pass validation later but ill do later...
        }
        public int getNextID(Dictionary<int, IAccountType> accountDB)
        {

            if (accountDB.Count() == 0)
            {
                return 1;
            }
            int nextBigID = accountDB.Keys.Max() + 1;
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
            // create a new Personal Bank Account and attach `travis` to the bank account with 100 bucks.
            IAccountOwner TravisAccount = new PersonalAccount(travis, new ChequingAccount(100.5, false));
            
            double travsBalance = TravisAccount.GetAccount().credit(50);

            Console.WriteLine("Balance:"+travsBalance);

            userRepo.CreateUser(travis);
        }
    }
}
