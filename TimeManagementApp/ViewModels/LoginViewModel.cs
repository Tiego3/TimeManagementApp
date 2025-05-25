using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Commands;
using TimeManagementApp.Services;
using TimeManagementApp.Views;
using TimeManagementLibrary;
using TimeManagementLibrary.Services;

namespace TimeManagementApp.ViewModels
{

    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public Func<string> PasswordAccessor { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private readonly AuthService _authService;

        public LoginViewModel()
        {
            var options = new DbContextOptionsBuilder<TimeManagementDbContext>()
                .UseSqlServer("your_connection_string_here")
                .Options;

            _authService = new AuthService(new TimeManagementDbContext(options));

            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(OpenRegister);
        }

        private void Login(object obj)
        {
            string password = PasswordAccessor?.Invoke();

            var user = _authService.Login(Username, password);
            if (user != null)
            {
                MessageBox.Show("Login successful.");
                // Open main app window...
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }

        private void OpenRegister(object obj)
        {
            var registerView = new RegisterWindow();
            registerView.Show();
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
