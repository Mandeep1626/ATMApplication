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
