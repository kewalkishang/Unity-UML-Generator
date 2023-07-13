using System;

public class BankAccount
{
    private string accountNumber;
    private Customer accountHolder;

    public string AccountNumber
    {
        get { return accountNumber; }
        set { accountNumber = value; }
    }

    public Customer AccountHolder
    {
        get { return accountHolder; }
        set { accountHolder = value; }
    }

    public BankAccount(string accountNumber, Customer accountHolder)
    {
        this.accountNumber = accountNumber;
        this.accountHolder = accountHolder;
    }

    public void Deposit(decimal amount)
    {
        // Deposit logic
    }

    public void Withdraw(decimal amount)
    {
        // Withdraw logic
    }
}