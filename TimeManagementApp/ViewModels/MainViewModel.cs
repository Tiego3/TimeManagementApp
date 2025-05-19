using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TimeManagementApp.Commands;
using TimeManagementLibrary;

namespace TimeManagementApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ModuleManager _moduleManager = new();

        // Form input properties
        public string NewModuleCode { get; set; }
        public string NewModuleName { get; set; }
        public int NewModuleCredits { get; set; }
        public int NewModuleClassHours { get; set; }
        public int NewModuleWeeks { get; set; }
        public DateTime NewModuleStartDate { get; set; } = DateTime.Today;

        public string SelectedModuleCode { get; set; }
        public DateTime StudyDate { get; set; } = DateTime.Today;
        public double HoursSpent { get; set; }

        public ObservableCollection<Module> Modules { get; } = new();

        public ICommand AddModuleCommand { get; }
        public ICommand AddStudyHoursCommand { get; }

        public MainViewModel()
        {
            AddModuleCommand = new RelayCommand(_ => AddModule());
            AddStudyHoursCommand = new RelayCommand(_ => AddStudyHours());
        }

        private void AddModule()
        {
            var module = new Module(NewModuleCode, NewModuleName, NewModuleCredits, NewModuleClassHours, NewModuleWeeks, NewModuleStartDate);
            _moduleManager.AddModule(module);
            Modules.Add(module);
            ClearModuleForm();
        }

        private void AddStudyHours()
        {
            var module = _moduleManager.GetModuleByCode(SelectedModuleCode);
            if (module != null)
            {
                module.AddStudyRecord(StudyDate, HoursSpent);
                OnPropertyChanged(nameof(Modules));
            }
        }

        private void ClearModuleForm()
        {
            NewModuleCode = string.Empty;
            NewModuleName = string.Empty;
            NewModuleCredits = 0;
            NewModuleClassHours = 0;
            NewModuleWeeks = 0;
            NewModuleStartDate = DateTime.Today;
            OnPropertyChanged(nameof(NewModuleCode));
            OnPropertyChanged(nameof(NewModuleName));
            OnPropertyChanged(nameof(NewModuleCredits));
            OnPropertyChanged(nameof(NewModuleClassHours));
            OnPropertyChanged(nameof(NewModuleWeeks));
            OnPropertyChanged(nameof(NewModuleStartDate));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
