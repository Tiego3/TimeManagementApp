using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TimeManagementLibrary;
using TimeManagementLibrary.Models;

namespace TimeManagementApp.Views
{
    public partial class ModulesListPage : Page
    {
        public ObservableCollection<Module> Modules { get; set; }

        public ModulesListPage(int userId)
        {
            InitializeComponent();

            using var db = new TimeManagementDbContext(DatabaseHelper.GetOptions());
            var userModules = db.Modules
                .Where(m => m.UserId == userId)
                .ToList();

            foreach (var module in userModules)
            {
                module.CalculateSelfStudyHoursPerWeek(); // Ensure it's calculated
            }

            Modules = new ObservableCollection<Module>(userModules);
            DataContext = this;
        }
    }
}
