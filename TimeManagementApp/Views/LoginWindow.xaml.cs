using System.Windows;
using System.Windows.Controls;
using TimeManagementApp.ViewModels;

namespace TimeManagementApp.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (DataContext is LoginViewModel vm)
            {
                bool loginSuccess = vm.Login(username, password);

                if (loginSuccess)
                {
                    MessageBox.Show("Login successful!");
                    // Navigate to MainWindow or dashboard here
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }
    }
}
