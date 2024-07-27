using System.Collections.Generic;
using System.Linq;

namespace Bank_Application_Assignment3
{
    public class Bank
    {
        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new List<Account>();
            for (int i = 100; i < 110; i++)
            {
                Accounts.Add(new Account(i, 100, 0.03, $"Account Holder {i}"));
            }
        }

        public Account RetrieveAccount(int accountNumber)
        {
            return Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }
    }
}
