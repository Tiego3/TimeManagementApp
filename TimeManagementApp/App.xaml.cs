using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TimeManagementLibrary;
using TimeManagementLibrary.Services;
using TimeManagementApp.Views;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Models;
using TimeManagementApp.ViewModels;
using System;

namespace TimeManagementApp
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDbContext<TimeManagementDbContext>(opt =>
                opt.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            services.AddSingleton<AuthService>();
            services.AddTransient<DataService>();

            services.AddTransient<LoginWindow>();
            services.AddTransient<RegisterWindow>();
            services.AddTransient<MainWindow>();

            Services = services.BuildServiceProvider();

            var loginWindow = Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            //protected override void OnStartup(StartupEventArgs e)
            //{
            //    var services = new ServiceCollection();

            //    services.AddDbContext<TimeManagementDbContext>(options =>
            //        options.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=TimeManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            //    services.AddScoped<AuthService>();

            //    serviceProvider = services.BuildServiceProvider();

            //    var loginWindow = new LoginWindow(serviceProvider.GetService<AuthService>());
            //    loginWindow.Show();
            //}
        }
    }
}
