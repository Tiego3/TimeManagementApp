using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TimeManagementLibrary.Models;
using TimeManagementLibrary;
using Microsoft.EntityFrameworkCore;

namespace TimeManagementApp.Views
{
    public partial class AddModulePage : Page
    {
        private readonly TimeManagementDbContext _context;
        private readonly int _currentUserId;

        public AddModulePage(TimeManagementDbContext context, int userId)
        {
            InitializeComponent();
            _context = context;
            _currentUserId = userId;

            LoadModules();
        }

        private void LoadModules()
        {
            var modules = _context.Modules
                .Where(m => m.UserId == _currentUserId)
                .AsEnumerable()
                .ToList();

            ModuleListView.ItemsSource = modules;
        }

        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CreditsBox.Text, out int credits) || !int.TryParse(ClassHoursBox.Text, out int classHours))
            {
                MessageBox.Show("Please enter valid numeric values.");
                return;
            }

            var module = new Module
            {
                Code = CodeBox.Text,
                Name = NameBox.Text,
                Credits = credits,
                ClassHoursPerWeek = classHours,
                UserId = _currentUserId,
                WeeksInSemester = 12, // Replace with actual value later
                StartDate = DateTime.Today // Replace with semester start date input
            };

            _context.Modules.Add(module);
            _context.SaveChanges();

            LoadModules(); // Refresh list
            NavigationService.Navigate(new ModulesListPage(_currentUserId));
        }
    }
}
