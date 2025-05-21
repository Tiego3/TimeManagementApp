using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using TimeManagementApp.Services;
using TimeManagementLibrary;

namespace TimeManagementApp.Views
{

   

    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthService(new TimeManagementDbContext());
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            var user = await _authService.LoginAsync(username, password);
            if (user != null)
            {
                Session.CurrentUser = user;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid login credentials.");
            }
        }
    }

}
