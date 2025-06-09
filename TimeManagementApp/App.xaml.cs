using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TimeManagementLibrary;
using TimeManagementLibrary.Services;
using TimeManagementApp.Views;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Models;

namespace TimeManagementApp
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public static User CurrentUser { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDbContext<TimeManagementDbContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=TimeManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            services.AddScoped<AuthService>();

            serviceProvider = services.BuildServiceProvider();

            var loginWindow = new LoginWindow(serviceProvider.GetService<AuthService>());
            loginWindow.Show();
        }
    }
}
