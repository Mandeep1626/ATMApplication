using System;
using System.Windows;
using System.Windows.Controls;

namespace Bank_Application_Assignment3
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Window prompt = new Window()
            {
                Width = 300,
                Height = 150,
                Title = caption
            };
            StackPanel stackPanel = new StackPanel();
            TextBlock textBlock = new TextBlock() { Text = text, Margin = new Thickness(10) };
            TextBox textBox = new TextBox() { Margin = new Thickness(10) };
            Button confirmation = new Button() { Content = "Ok", Margin = new Thickness(10) };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(confirmation);
            prompt.Content = stackPanel;
            prompt.ShowDialog();

            return textBox.Text;
        }
    }
}

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
using System;
using System.Windows;

namespace Bank_Application_Assignment3
{
    public partial class MainWindow : Window
    {
        private AtmApplication _atmApplication;

        public MainWindow()
        {
            InitializeComponent();
            _atmApplication = new AtmApplication();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            var accountNumber = Convert.ToInt32(Prompt.ShowDialog("Enter Account Number:", "Create Account"));
            var initialBalance = Convert.ToDouble(Prompt.ShowDialog("Enter Initial Balance:", "Create Account"));
            var interestRate = Convert.ToDouble(Prompt.ShowDialog("Enter Interest Rate:", "Create Account"));
            var accountHolderName = Prompt.ShowDialog("Enter Account Holder's Name:", "Create Account");

            _atmApplication.CreateAccount(accountNumber, initialBalance, interestRate, accountHolderName);
            MessageBox.Show("Account created successfully!");
        }

        private void SelectAccount_Click(object sender, RoutedEventArgs e)
        {
            var accountNumber = Convert.ToInt32(Prompt.ShowDialog("Enter Account Number:", "Select Account"));
            try
            {
                _atmApplication.SelectAccount(accountNumber);
                AccountMenu.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CheckBalance_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_atmApplication.CheckBalance());
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            var amount = Convert.ToDouble(Prompt.ShowDialog("Enter Amount to Deposit:", "Deposit"));
            _atmApplication.Deposit(amount);
            MessageBox.Show("Deposit successful!");
        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {
            var amount = Convert.ToDouble(Prompt.ShowDialog("Enter Amount to Withdraw:", "Withdraw"));
            try
            {
                _atmApplication.Withdraw(amount);
                MessageBox.Show("Withdrawal successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayTransactions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_atmApplication.DisplayTransactions());
        }

        private void ExitAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountMenu.Visibility = Visibility.Collapsed;
        }
    }
}
