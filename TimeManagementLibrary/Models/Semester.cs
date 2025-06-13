using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TimeManagementApp.Models;

namespace TimeManagementLibrary.Models
{
    public class Semester : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<Module> Modules { get; set; } = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
