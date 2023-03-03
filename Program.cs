namespace Bank_Account_Task;
using System;
using System.Collections.Generic;

public class BankAccount
{
	public int AccountNumber {get; set;}
	public double Balance {get; set;}
	public List <Transaction> Transactions {get; set;}
	
	public BankAccount(int accountNumber, double balance)
	{
		AccountNumber = accountNumber;
		Balance = balance;
		Transactions = new List <Transaction>();
	}
	
	public void Deposit (double amount)
	{
		if(amount <= 0){
			throw new ArgumentException("You can not deposit a negative amount.");
		}
		Balance += amount;
		Transactions.Add(new Transaction(amount, TransactionType.Deposit));
	}
	
	public void Withdraw(double amount)
	{
		if (amount <= 0){
			throw new ArgumentException("You can not withdraw a negative amount.");
		}
		
		if(amount > Balance){
			throw new InvalidOperationException("Insufficient funds");
		}
		
		Balance -= amount;
		Transactions.Add(new Transaction(amount, TransactionType.Withdrawal));
	}
}

public class Transaction
{
	public double Amount {get; set;}
	public DateTime Date {get; set;}
	public TransactionType Type {get; set;}
	
	public Transaction (double amount, TransactionType type)
	{
		Amount = amount;
		Type = type;
		Date = DateTime.Now;
	}
}

public enum TransactionType
{
	Deposit,
	Withdrawal
}

public class Program
{
	public static void Main(string[] args)
	{
		List<BankAccount> accounts = new List<BankAccount>();
		while(true)
		{
			Console.WriteLine("Enter 1 to create a new account.");
			Console.WriteLine("Enter 2 to view an account.");
			Console.WriteLine("Enter 3 to deposit or withdraw funds.");
			Console.WriteLine("Enter 4 to list all accounts.");
			
			int option = int.Parse(Console.ReadLine());
			
			switch(option)
			{
				case 1: 
					AddAccount(accounts);
					break;
				case 2:
					ViewAccount(accounts);
					break;
				case 3:
					DepositOrWithdrawFunds(accounts);
					break;
				case 4:
					ListAllAccounts(accounts);
					break;
				default:
					Console.WriteLine("Invalid Choice");
					break;	
			}
		}
	}
		
	static void AddAccount(List<BankAccount> account)
	{
		Console.Write("Enter account number: ");
		int accountNumber = int.Parse(Console.ReadLine());

		if (account.Exists(a => a.AccountNumber == accountNumber)){
			throw new InvalidOperationException("Account number already exists.");
		}

		Console.Write("Enter initial balance.");
		double initialBalance = double.Parse(Console.ReadLine());

		BankAccount acc = new BankAccount(accountNumber, initialBalance);
		account.Add(acc);
		
		Console.WriteLine("Account created successfully.");
	}
	
	static void ViewAccount(List<BankAccount> account)
	{
		Console.WriteLine("Enter your account number");
		int accountNumber = int.Parse(Console.ReadLine());
		
		var validAccount = account.Find(a => a.AccountNumber == accountNumber);
		
		if(validAccount == null)
        {
			throw new ArgumentNullException("Invalid account number.");
		}
        
        Console.WriteLine("Account Number: " + validAccount.AccountNumber + " Account Balance: " + validAccount.Balance);
        Console.WriteLine("Transactions:"); 
        
        foreach(var trans in validAccount.Transactions){
            Console.WriteLine("Date: " + trans.Date + " Type: " + trans.Type + " Amonut: " + trans.Amount);
        }
	}
	
    static void DepositFunds(BankAccount bankAccount){
        Console.WriteLine("Enter amount to deposit into account " + bankAccount.AccountNumber);
        double depositAmount = double.Parse(Console.ReadLine());
        
        bankAccount.Deposit(depositAmount);
        
        Console.WriteLine("Deposit successful, your new balance is: " + bankAccount.Balance);
    }

    static void WithdrawFunds(BankAccount bankAccount){
         Console.WriteLine("Enter amount to withdraw from account: " + bankAccount.AccountNumber);
        double withdrawlAmount = double.Parse(Console.ReadLine());
        
        bankAccount.Withdraw(withdrawlAmount);
        
        Console.WriteLine("Withdrawl successful, your new balance is: " + bankAccount.Balance);
    }

	static void DepositOrWithdrawFunds (List<BankAccount> account){
		Console.WriteLine("Enter your account number");
		int accountNumber = int.Parse(Console.ReadLine());
		
		var validAccount = account.Find(a => a.AccountNumber == accountNumber);
		
		if(validAccount == null){
			throw new ArgumentNullException("Invalid account number.");
		}

        Console.WriteLine("Enter 1 to make a deposit."); 
        Console.WriteLine("Enter 2 to make a withdrawl.");
        int transactionType = int.Parse(Console.ReadLine());
        
        if(transactionType == 1){
            DepositFunds(validAccount);
        } else if(transactionType == 2) {
           WithdrawFunds(validAccount);
        } else {
            throw new ArgumentException("Invalid Transaction type.");
        }
	}
	
	static void ListAllAccounts (List<BankAccount> account){
		if(account == null){
			throw new ArgumentNullException("There are no accounts");
		}

        foreach(var acc in account){
            Console.WriteLine("Account Number: " + acc.AccountNumber + " Account Balance: " + acc.Balance);
        }
	}
}