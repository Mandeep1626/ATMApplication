using System;
using System.Linq;

namespace Bank_Application_Assignment3
{
    public class AtmApplication
    {
        private Bank _bank;
        private Account _selectedAccount;

        public AtmApplication()
        {
            _bank = new Bank();
        }

        public void CreateAccount(int accountNumber, double initialBalance, double interestRate, string accountHolderName)
        {
            var account = new Account(accountNumber, initialBalance, interestRate, accountHolderName);
            _bank.AddAccount(account);
        }

        public void SelectAccount(int accountNumber)
        {
            _selectedAccount = _bank.RetrieveAccount(accountNumber);
            if (_selectedAccount == null)
            {
                throw new InvalidOperationException("Account not found.");
            }
        }

        public string CheckBalance()
        {
            return _selectedAccount.CheckBalance();
        }

        public void Deposit(double amount)
        {
            _selectedAccount.Deposit(amount);
        }

        public void Withdraw(double amount)
        {
            _selectedAccount.Withdraw(amount);
        }

        public string DisplayTransactions()
        {
            return string.Join(Environment.NewLine, _selectedAccount.Transactions);
        }
    }
}
