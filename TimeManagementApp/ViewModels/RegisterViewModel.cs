using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Text;
using System;
using TimeManagementApp.Commands;
using TimeManagementLibrary;
using TimeManagementApp.Service;



namespace TimeManagementApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _statusMessage;

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

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterUser);
        }

        private void RegisterUser(object obj)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = "Please enter a username and password.";
                return;
            }

            string hashedPassword = HashPassword(Password);

            // Store to DB (call service or directly use EF Core)
            bool result = UserService.RegisterUser(Username, hashedPassword);

            StatusMessage = result ? "Registration successful." : "Username already exists.";
        }

        private string HashPassword(string password)
        {
            using SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
