using System.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using TimeManagementApp.Commands;
using TimeManagementApp.ViewModels;
using TimeManagementLibrary.Services;
using TimeManagementLibrary;

namespace TimeManagementApp.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            var vm = new RegisterViewModel();
            this.DataContext = vm;
            vm.PasswordAccessor = () => PasswordBox.Password;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) { }
    }
}
