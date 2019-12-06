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

        public String getName(){
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
        int getBalance();
        int credit(int amount);
        int debit(int amount);
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
    interface ITableRepo {
        // we can leave empty for now
    }

    // Doing this to keep it abstract...abstration is the an ally.
    interface IAccountOwner
    {
        String getAccountOwnerName();
        int getAccountBalance(IAccountType accountType);
    }

    class PersonalAccount : IAccountOwner
    {

// Stringly coupled `PersonAccount + Person` meh kinda a code smell but can refactor later once working...
// easy fix create interface maybe...donno will get back to later.
        Person accountOwner;
        PersonalAccount(Person accountOwnerIn) => this.accountOwner = accountOwnerIn;

        public int getAccountBalance(IAccountType accountType)
        {
            return accountType.getBalance();
        }

        public string getAccountOwnerName()
        {
            return this.accountOwner.getName();
        }
    }

    class ChequingAccount : IAccountType
    {

        
        // Some user object that `OWNS` the `ACCOUNT` we need to create the use class.
        // This is a BANK account
        public int credit(int amount)
        {
            throw new NotImplementedException();
        }

        public int debit(int amount)
        {
            throw new NotImplementedException();
        }

        public int getBalance()
        {
            throw new NotImplementedException();
        }
    }

    class UserTable : ITableRepo {
        Dictionary<int, Person> usersDB = new Dictionary<int, Person>();

    }

    interface IUserRepo {
        int CreateUser(Person user);
        bool DeleteUser(int userId);
        Person getUser(int userId);
    }

    // Add the DB stuff here
    class InMemoryUserDB : IUserRepo
    {
        Dictionary<int, Person> userDB = new Dictionary<int, Person>();

        public InMemoryUserDB(){}
        public int CreateUser(Person user)
        {
            userDB.Add(getNextID(userDB), user);
            Console.WriteLine("Created User" + user.getName());
            return userDB.Keys.Max();
        }

        public int getNextID(Dictionary<int, Person> userDB) {

            if(userDB.Count() == 0) {
                return 1;
            }
            int nextBigID = userDB.Keys.Max()+1;
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

    class BigBadBank {

        static void Main(string[] args)
        {
            IUserRepo userRepo = new InMemoryUserDB();

            Person travis = new Person("Travis", 22);

            userRepo.CreateUser(travis);
        }
    }
}
