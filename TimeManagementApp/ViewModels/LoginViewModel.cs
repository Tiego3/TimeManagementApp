using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TimeManagementApp.Commands;
using TimeManagementApp.Services;
using TimeManagementApp.Views;


namespace TimeManagementApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        private readonly UserService _userService;

        public LoginViewModel()
        {
            _userService = new UserService();

            LoginCommand = new RelayCommand(ExecuteLogin, CanLogin);
            NavigateToRegisterCommand = new RelayCommand(OpenRegisterWindow);
        }

        private bool CanLogin(object parameter) =>
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        private async void ExecuteLogin(object parameter)
        {
            try
            {
                var userId = await _userService.AuthenticateUserAsync(Username, Password);

                if (userId.HasValue)
                {
                    // Set the session user ID
                    Session.CurrentUserId = userId.Value;

                    var mainWindow = new MainWindow();
                    Application.Current.Windows[0]?.Close(); // Close LoginWindow
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenRegisterWindow(object parameter)
        {
            var registerWindow = new RegisterWindow();
            Application.Current.Windows[0]?.Close(); // Close LoginWindow
            registerWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
