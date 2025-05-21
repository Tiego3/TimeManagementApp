using System.ComponentModel;
using System.Runtime.CompilerServices;
using TimeManagementApp.Services;
using TimeManagementLibrary;

namespace TimeManagementApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService = new();
        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool Login(string username, string password)
        {
            var user = _userService.AuthenticateUser(username, password);

            if (user != null)
            {
                // Save current user if needed
                ErrorMessage = string.Empty;
                return true;
            }

            ErrorMessage = "Invalid username or password.";
            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
