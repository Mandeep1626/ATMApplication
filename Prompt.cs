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
