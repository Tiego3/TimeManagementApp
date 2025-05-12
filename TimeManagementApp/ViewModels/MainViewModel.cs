using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using TimeManagementApp.Models; // Assuming your Module class is here
using TimeManagementApp.ViewModels;
using TimeManagementLibrary;
using static System.Net.Mime.MediaTypeNames;

namespace TimeManagementApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ModuleManager _manager = new ModuleManager();
        public ObservableCollection<Module> Modules { get; set; } = new ObservableCollection<Module>();

        // Form input properties
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Credits { get; set; } = string.Empty;
        public string ClassHoursPW { get; set; } = string.Empty;
        public string WeeksInSemester { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Today;

        // Commands
        public ICommand AddModuleCommand { get; }

        public MainViewModel()
        {
            AddModuleCommand = new RelayCommand(AddModule);
        }

        private void AddModule()
        {
            if (!int.TryParse(Credits, out int credits) ||
                !int.TryParse(ClassHoursPW, out int classHoursPW) ||
                !int.TryParse(WeeksInSemester, out int weeks))
                return;

            var module = new Module(Code, Name, credits, classHoursPW, weeks, StartDate);
            Modules.Add(module);
            _manager.AddModule(module);

            // Reset form
            Code = Name = Credits = ClassHoursPW = WeeksInSemester = string.Empty;
            StartDate = DateTime.Today;
            OnPropertyChanged("");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        public event EventHandler CanExecuteChanged;
        public RelayCommand(Action execute) => _execute = execute;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
    }
}
