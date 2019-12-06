# csharp_refresh
just refreshing my cs coding

added some comments of my thoughts as I was coding... NOT DONE YET

OUTPUT
```
➜  csharp git:(master) ✗ dotnet run                                                  
Balance:150.5
Created UserTravis
➜  csharp git:(master) ✗ 
```

# Let me explain whats happening 

```
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
```

1) I create my repos not really needed yet but thinking about the bigger picture later i want to save stuff.
2) I create my user to that will have a Bank account.
3) Create the new `PersonalAccount` with user `travis, 22`
4) Get my account since I didn't expose this directly we need to hit the TravisAccount object first then credit the account `+50$`
5) Did my code work? all the objects work and communicate correctly? spolier `yes` they did.
6) create my user just to see if my user repo works...

- SBT