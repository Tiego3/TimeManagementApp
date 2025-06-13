// LoginWindow.xaml.cs
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TimeManagementApp.Views;
using TimeManagementLibrary.Services;

namespace TimeManagementApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;
        private App App => (App)Application.Current;

        public LoginWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = await _authService.Login(username, password);
            if (user != null)
            {
                // Open MainWindow with logged-in user
                var mainWindow = App.Services.GetRequiredService<MainWindow>();
                mainWindow.SetLoggedInUser(user);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageTextBlock.Text = "Login failed. Check credentials.";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var wnd = App.Services.GetRequiredService<RegisterWindow>();
            wnd.Show();
            Close();
        }
    }
}
