using System.Windows;
using System.Windows.Input;
using TimeManagementLibrary.Services;
using TimeManagementApp.Models;
using TimeManagementApp.Commands;
using TimeManagementApp;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeManagementApp.ViewModels
{
    // ViewModel for Login functionality

    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        //public string Username { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login(object passwordObj)
        {
            var password = passwordObj as string;
            var user = _authService.Login(Username, password);

            if (user != null)
            {
                App.CurrentUser = user;
                MessageBox.Show("Login successful!");
                // Navigate to MainWindow
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }

        private void Register(object passwordObj)
        {
            var password = passwordObj as string;
            bool success = _authService.Register(Username, password);

            if (success)
                MessageBox.Show("Registration successful!");
            else
                MessageBox.Show("Username already taken.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }


    }
}
