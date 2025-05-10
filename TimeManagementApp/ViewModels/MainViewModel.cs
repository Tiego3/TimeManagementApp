using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TimeManagementLibrary;

namespace TimeManagementApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Module> Modules { get; set; } = new ObservableCollection<Module>();

        private ModuleManager _manager = new ModuleManager();

        private int _totalStudyHours;
        public int TotalStudyHours
        {
            get => _totalStudyHours;
            set
            {
                _totalStudyHours = value;
                OnPropertyChanged(nameof(TotalStudyHours));
            }
        }

        public MainViewModel()
        {
            // Sample data
            _manager.AddModule(new Module { Name = "Mathematics", Credits = 12 });
            _manager.AddModule(new Module { Name = "Physics", Credits = 10 });

            foreach (var mod in _manager.GetAllModules())
                Modules.Add(mod);

            TotalStudyHours = _manager.CalculateTotalStudyHours();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    
    }
}
