using System.Windows;
using TimeManagementLibrary.Services;
using TimeManagementApp.Views;
using TimeManagementLibrary;
using System.Windows.Navigation;

namespace TimeManagementApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;

        public LoginWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = _authService.Login(username, password);

            if (user != null)
            {
                var mainWindow = new MainWindow(user); // Pass user to main window
                mainWindow.Show();
                this.Close();
                NavigationService.Navigate(new ModulesListPage(loggedInUser.Id));

            }
            else
            {
                MessageTextBlock.Text = "Invalid credentials.";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            bool success = _authService.Register(username, password);

            if (success)
            {
                MessageTextBlock.Text = "Registration successful. You may now log in.";
            }
            else
            {
                MessageTextBlock.Text = "Username already exists.";
            }
        }
    }
}
