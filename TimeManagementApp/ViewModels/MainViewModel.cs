using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TimeManagementApp.Commands;
using TimeManagementLibrary;

namespace TimeManagementApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // MODULE ADDING
        public string NewModuleCode { get; set; }
        public string NewModuleName { get; set; }
        public string NewModuleCredits { get; set; }
        public string NewModuleClassHours { get; set; }
        public string NewModuleWeeks { get; set; }
        public DateTime? NewModuleStartDate { get; set; }

        public ObservableCollection<Module> Modules { get; set; } = new();

        // MODULE SELECTION
        private Module _selectedModule;
        public Module SelectedModule
        {
            get => _selectedModule;
            set
            {
                _selectedModule = value;
                OnPropertyChanged();
            }
        }

        // STUDY RECORD
        public string NewStudyHours { get; set; }
        public DateTime? NewStudyDate { get; set; }

        // COMMANDS
        public ICommand AddModuleCommand { get; }
        public ICommand RecordStudyCommand { get; }

        public MainViewModel()
        {
            AddModuleCommand = new RelayCommand(AddModule);
            RecordStudyCommand = new RelayCommand(RecordStudyHours);
        }

        private void AddModule()
        {
            // Validation
            if (!int.TryParse(NewModuleCredits, out int credits) ||
                !int.TryParse(NewModuleClassHours, out int classHours) ||
                !int.TryParse(NewModuleWeeks, out int weeks) ||
                NewModuleStartDate == null)
            {
                MessageBox.Show("Please enter valid module details.");
                return;
            }

            var module = new Module(
                NewModuleCode,
                NewModuleName,
                credits,
                classHours,
                weeks,
                NewModuleStartDate.Value
            );

            Modules.Add(module);

            // Clear fields
            NewModuleCode = NewModuleName = NewModuleCredits = NewModuleClassHours = NewModuleWeeks = "";
            NewModuleStartDate = null;
            OnPropertyChanged(nameof(NewModuleCode));
            OnPropertyChanged(nameof(NewModuleName));
            OnPropertyChanged(nameof(NewModuleCredits));
            OnPropertyChanged(nameof(NewModuleClassHours));
            OnPropertyChanged(nameof(NewModuleWeeks));
            OnPropertyChanged(nameof(NewModuleStartDate));
        }

        private void RecordStudyHours()
        {
            if (SelectedModule == null)
            {
                MessageBox.Show("Please select a module.");
                return;
            }

            if (!double.TryParse(NewStudyHours, out double hours))
            {
                MessageBox.Show("Invalid number of study hours.");
                return;
            }

            var record = new StudyTimeRecord
            {
                StudyDate = NewStudyDate ?? DateTime.Now,
                HoursSpent = hours
            };

            SelectedModule.StudyTimeRecords.Add(record);

            // Clear inputs
            NewStudyHours = "";
            NewStudyDate = null;
            OnPropertyChanged(nameof(NewStudyHours));
            OnPropertyChanged(nameof(NewStudyDate));
            OnPropertyChanged(nameof(SelectedModule)); 
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
