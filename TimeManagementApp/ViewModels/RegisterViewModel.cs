using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeManagementApp.Commands;
using TimeManagementApp.Views;
using TimeManagementLibrary;
using TimeManagementLibrary.Services;

namespace TimeManagementApp.ViewModels
{

    public class RegisterViewModel : BaseViewModel
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public ICommand RegisterCommand { get; }
        private readonly AuthService _authService;
        private readonly IServiceProvider _sp;

        public RegisterViewModel(AuthService authService, IServiceProvider sp)
        {
            _authService = authService;
            _sp = sp;
            RegisterCommand = new RelayCommand(async _ => await Register());
        }

        private async Task Register()
        {
            var ok = await Task.Run(() => _authService.Register(Username, Password));
            if (ok)
            {
                MessageBox.Show("Registered. Please login.");
                _sp.GetRequiredService<LoginWindow>().Show();
                ((Window)App.Current.MainWindow!).Close();
            }
            else MessageBox.Show("Username exists.");
        }
    }

    //public class RegisterViewModel : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public string Username { get; set; }
    //    public Func<string> PasswordAccessor { get; set; }

    //    public ICommand RegisterCommand { get; }

    //    private readonly AuthService _authService;

    //    public RegisterViewModel()
    //    {
    //        var options = new DbContextOptionsBuilder<TimeManagementDbContext>()
    //            .UseSqlServer("your_connection_string_here")
    //            .Options;

    //        _authService = new AuthService(new TimeManagementDbContext(options));
    //        RegisterCommand = new RelayCommand(Register);
    //    }

    //    private void Register(object obj)
    //    {
    //        string password = PasswordAccessor?.Invoke();

    //        if (_authService.Register(Username, password))
    //        {
    //            MessageBox.Show("Registration successful!");
    //        }
    //        else
    //        {
    //            MessageBox.Show("Username already exists.");
    //        }
    //    }

    //    private void OnPropertyChanged(string prop) =>
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //}
}
