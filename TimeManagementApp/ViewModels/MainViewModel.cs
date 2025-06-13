using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Commands;
using TimeManagementApp.Models;
using TimeManagementLibrary;
using TimeManagementLibrary.Models;
using TimeManagementLibrary.Services;

namespace TimeManagementApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public DateTime SemesterStart { get; set; }
        public int SemesterWeeks { get; set; }
        public ObservableCollection<Module> Modules { get; } = new();
        public string NewCode { get; set; } = "";
        public string NewName { get; set; } = "";
        public int NewCredits { get; set; }
        public int NewClassHours { get; set; }
        public string SelectedModuleCode { get; set; } = "";
        public DateTime StudyDate { get; set; } = DateTime.Today;
        public double StudyHours { get; set; }

        public ICommand SaveSemesterCommand { get; }
        public ICommand AddModuleCommand { get; }
        public ICommand AddStudyCommand { get; }
        public ICommand RefreshCommand { get; }

        private readonly DataService _dataService;
        private User _user;


        public MainViewModel(DataService ds)
        {
            _dataService = ds;
            SaveSemesterCommand = new RelayCommand(async _ => await SaveSemester());
            AddModuleCommand = new RelayCommand(async _ => await AddModule());
            AddStudyCommand = new RelayCommand(async _ => await AddStudy());
            RefreshCommand = new RelayCommand(async _ => await LoadModules());
            _ = LoadInitialData();
        }

        private async Task LoadInitialData()
        {
            var mods = await Task.Run(() => _dataService.GetModulesWithRecords(_user.Id));
            SemesterStart = mods.FirstOrDefault()?.Semester.StartDate ?? DateTime.Today;
            SemesterWeeks = mods.FirstOrDefault()?.Semester.NumberOfWeeks ?? 12;
            Modules.Clear();
            foreach (var m in mods) Modules.Add(m);
            Notify(nameof(SemesterStart));
            Notify(nameof(SemesterWeeks));
        }

        private async Task SaveSemester()
            => await Task.Run(() => _dataService.SaveSemester(_user.Id, SemesterStart, SemesterWeeks));

        private async Task AddModule()
        {
            var m = new TimeManagementLibrary.Models.Module
            {
                Code = NewCode,
                Name = NewName,
                Credits = NewCredits,
                ClassHoursPerWeek = NewClassHours
            };
            await Task.Run(() => _dataService.AddModule(_user.Id, m));
            await LoadInitialData();
        }

        private async Task AddStudy()
        {
            var m = Modules.SingleOrDefault(x => x.Code == SelectedModuleCode);
            if (m != null)
                await Task.Run(() => _dataService.AddStudyRecord(m.Id, StudyDate, StudyHours));
            await LoadInitialData();
        }

        public void SetUser(User user)
        {
            _user = user;
            _ = LoadInitialData();
        }


        private async Task LoadModules()
            => await LoadInitialData();
    }

    //public class MainViewModel : INotifyPropertyChanged
    //{
    //    private readonly ModuleManager _moduleManager = new();
    //    public ObservableCollection<Module> Modules { get; set; }

    //    // Form input properties
    //    public string NewModuleCode { get; set; }
    //    public string NewModuleName { get; set; }
    //    public int NewModuleCredits { get; set; }
    //    public int NewModuleClassHours { get; set; }
    //    public int NewModuleWeeks { get; set; }
    //    public DateTime NewModuleStartDate { get; set; } = DateTime.Today;

    //    public string SelectedModuleCode { get; set; }
    //    public DateTime StudyDate { get; set; } = DateTime.Today;
    //    public double HoursSpent { get; set; }

    //    public ObservableCollection<Module> Modules { get; } = new();

    //    public ICommand AddModuleCommand { get; }
    //    public ICommand AddStudyHoursCommand { get; }

    //    public MainViewModel()
    //    {
    //        AddModuleCommand = new RelayCommand(_ => AddModule());
    //        AddStudyHoursCommand = new RelayCommand(_ => AddStudyHours());
    //    }
    //    public MainViewModel(User user)
    //    {
    //        using var db = new TimeManagementDbContext(DatabaseHelper.GetOptions());

    //        var userModules = db.Modules
    //            .Include(m => m.StudyTimeRecords)
    //            .Where(m => m.UserId == user.Id)
    //            .ToList();

    //        foreach (var module in userModules)
    //        {
    //            // Ensure RemainingHoursThisWeek is triggered
    //            module.PropertyChanged?.Invoke(module, new PropertyChangedEventArgs(nameof(Module.RemainingHoursThisWeek)));
    //        }

    //    }
    //    private void AddModule()
    //    {
    //        var module = new Module(NewModuleCode, NewModuleName, NewModuleCredits, NewModuleClassHours, NewModuleWeeks, NewModuleStartDate);
    //        _moduleManager.AddModule(module);
    //        Modules.Add(module);
    //        ClearModuleForm();
    //    }

    //    private void AddStudyHours()
    //    {
    //        var module = _moduleManager.GetModuleByCode(SelectedModuleCode);
    //        if (module != null)
    //        {
    //            module.AddStudyRecord(StudyDate, HoursSpent);
    //            OnPropertyChanged(nameof(Modules));
    //        }
    //    }

    //    private void ClearModuleForm()
    //    {
    //        NewModuleCode = string.Empty;
    //        NewModuleName = string.Empty;
    //        NewModuleCredits = 0;
    //        NewModuleClassHours = 0;
    //        NewModuleWeeks = 0;
    //        NewModuleStartDate = DateTime.Today;
    //        OnPropertyChanged(nameof(NewModuleCode));
    //        OnPropertyChanged(nameof(NewModuleName));
    //        OnPropertyChanged(nameof(NewModuleCredits));
    //        OnPropertyChanged(nameof(NewModuleClassHours));
    //        OnPropertyChanged(nameof(NewModuleWeeks));
    //        OnPropertyChanged(nameof(NewModuleStartDate));
    //    }

    //    public event PropertyChangedEventHandler? PropertyChanged;
    //    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    //        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    //}
}
