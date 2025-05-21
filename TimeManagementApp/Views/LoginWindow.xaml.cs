using System;
using System.Windows;
using System.Windows.Controls;
using TimeManagementApp.Services;
using TimeManagementLibrary;
using TimeManagementApp.ViewModels;

namespace TimeManagementApp.Views
{ 

    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthService(new TimeManagementDbContext());
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            var user = await _authService.LoginAsync(username, password);
            if (user != null)
            {
                Session.CurrentUser = user;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid login credentials.");
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }

    }

}
