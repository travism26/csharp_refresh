using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{

    interface IUserRepo
    {
        int CreateUser(Person user); // this can be further abstracted to an interface.
        bool DeleteUser(int userId);
        Person getUser(int userId); // this can be further abstracted to an interface.
    }

    // Add the DB stuff here
    class InMemoryUserDB : IUserRepo
    {
        Dictionary<int, Person> userDB = new Dictionary<int, Person>();

        public InMemoryUserDB() { }
        public int CreateUser(Person user)
        {
            int userId = getNextID(userDB);
            userDB.Add(userId, user);
            Console.WriteLine("Created User:" + user.getName());
            return userId;
        }

        //this method can be a helper method abstracted out since ill be using in a few places.
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
}