namespace csharp
{

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

        public double getAccountBalance(IAccountType accountType)
        {
            return accountType.getBalance();
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