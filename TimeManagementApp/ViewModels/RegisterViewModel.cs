using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Commands;
using TimeManagementLibrary;
using TimeManagementLibrary.Services;

namespace TimeManagementApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public Func<string> PasswordAccessor { get; set; }

        public ICommand RegisterCommand { get; }

        private readonly AuthService _authService;

        public RegisterViewModel()
        {
            var options = new DbContextOptionsBuilder<TimeManagementDbContext>()
                .UseSqlServer("your_connection_string_here")
                .Options;

            _authService = new AuthService(new TimeManagementDbContext(options));
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object obj)
        {
            string password = PasswordAccessor?.Invoke();

            if (_authService.Register(Username, password))
            {
                MessageBox.Show("Registration successful!");
            }
            else
            {
                MessageBox.Show("Username already exists.");
            }
        }

        private void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
