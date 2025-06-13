using System.Windows;
using System.Windows.Input;
using TimeManagementLibrary.Services;
using TimeManagementApp.Models;
using TimeManagementApp.Commands;
using TimeManagementApp;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;
using TimeManagementApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace TimeManagementApp.ViewModels
{

    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public ICommand LoginCommand { get; }
        public ICommand RegisterNavCommand { get; }

        private readonly AuthService _authService;
        private readonly IServiceProvider _sp;

        public LoginViewModel(AuthService authService, IServiceProvider sp)
        {
            _authService = authService;
            _sp = sp;
            LoginCommand = new RelayCommand(async _ => await Login());
            RegisterNavCommand = new RelayCommand(_ => NavigateToRegister());
        }

        private async Task Login()
        {
            var u = await Task.Run(() => _authService.Login(Username, Password));
            if (u != null)
            {
                var mw = _sp.GetRequiredService<MainWindow>();
                mw.DataContext = _sp.GetRequiredService<MainViewModel>();
                mw.Show();
                ((Window)App.Current.MainWindow!).Close();
            }
            else MessageBox.Show("Login failed.");
        }

        private void NavigateToRegister()
        {
            _sp.GetRequiredService<RegisterWindow>().Show();
            ((Window)App.Current.MainWindow!).Close();
        }
    }

    //public class LoginViewModel : INotifyPropertyChanged
    //{
    //    private readonly AuthService _authService;


    //    public ICommand LoginCommand { get; }
    //    public ICommand RegisterCommand { get; }

    //    public LoginViewModel(AuthService authService)
    //    {
    //        _authService = authService;
    //        LoginCommand = new RelayCommand(Login);
    //        RegisterCommand = new RelayCommand(Register);
    //    }

    //    private void Login(object passwordObj)
    //    {
    //        var password = passwordObj as string;
    //        var user = _authService.Login(Username, password);

    //        if (user != null)
    //        {
    //            App.CurrentUser = user;
    //            MessageBox.Show("Login successful!");
    //            // Navigate to MainWindow
    //        }
    //        else
    //        {
    //            MessageBox.Show("Invalid credentials.");
    //        }
    //    }

    //    private void Register(object passwordObj)
    //    {
    //        var password = passwordObj as string;
    //        bool success = _authService.Register(Username, password);

    //        if (success)
    //            MessageBox.Show("Registration successful!");
    //        else
    //            MessageBox.Show("Username already taken.");
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private void OnPropertyChanged([CallerMemberName] string name = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    //    }

    //    private string _username;
    //    public string Username
    //    {
    //        get => _username;
    //        set
    //        {
    //            _username = value;
    //            OnPropertyChanged();
    //        }
    //    }


    //}
}
