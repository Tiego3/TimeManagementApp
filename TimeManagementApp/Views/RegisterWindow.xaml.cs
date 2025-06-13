// RegisterWindow.xaml.cs
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TimeManagementLibrary.Services;

namespace TimeManagementApp.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly AuthService _authService;
        private App App => (App)Application.Current;

        public RegisterWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // no VM binding here
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            bool success = await _authService.Register(username, password);
            MessageBox.Show(success ? "Registered! You may now log in." : "Username already exists.");

            if (success)
            {
                var login = App.Services.GetRequiredService<LoginWindow>();
                login.Show();
                Close();
            }
        }
    }
}
