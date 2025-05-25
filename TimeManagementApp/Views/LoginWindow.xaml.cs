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
            var vm = new LoginViewModel();
            this.DataContext = vm;
            vm.PasswordAccessor = () => PasswordBox.Password;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // No action needed unless you want live validation
        }
    }

}
