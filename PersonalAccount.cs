using System;

namespace csharp
{

    // Doing this to keep it abstract...abstration is the an ally.
    // since there can be multiple type of accounts: Personal, Business, private? ...etc
    interface IAccountOwner
    {
        String getAccountOwnerName();
        double getAccountBalance();
        IAccountType GetAccount();
    }


    class PersonalAccount : IAccountOwner
    {

        // Stringly coupled `PersonAccount + Person` meh kinda a code smell but can refactor later once working...
        // easy fix create interface maybe...donno will get back to later.
        Person accountOwner;
        IAccountType accountType;
        public PersonalAccount(Person accountOwnerIn, IAccountType accountTypeIn)
        {
            this.accountOwner = accountOwnerIn;
            this.accountType = accountTypeIn;
        }

        public double getAccountBalance()
        {
            return this.accountType.getBalance();
        }

        public string getAccountOwnerName()
        {
            return this.accountOwner.getName();
        }

        public IAccountType GetAccount()
        {
            return this.accountType;
        }
    }

}