using System;
public class Customer
{
    private string name;
    private BankAccount account;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public BankAccount Account
    {
        get { return account; }
        set { account = value; }
    }

    public Customer(string name, BankAccount account)
    {
        this.name = name;
        this.account = account;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Customer Name: " + name);
        Console.WriteLine("Account Number: " + account.AccountNumber);
    }
}