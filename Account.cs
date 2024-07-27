using System;
using System.Collections.Generic;

namespace Bank_Application_Assignment3
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public double Balance { get; set; }
        public double InterestRate { get; set; }
        public string AccountHolderName { get; set; }
        public List<string> Transactions { get; set; }

        public Account(int accountNumber, double initialBalance, double interestRate, string accountHolderName)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            InterestRate = interestRate;
            AccountHolderName = accountHolderName;
            Transactions = new List<string>();
        }

        public void Deposit(double amount)
        {
            Balance += amount;
            Transactions.Add($"Deposited: {amount:C}");
        }

        public void Withdraw(double amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Transactions.Add($"Withdrew: {amount:C}");
            }
            else
            {
                throw new InvalidOperationException("Insufficient balance.");
            }
        }

        public string CheckBalance()
        {
            return $"Balance: {Balance:C}";
        }
    }
}
