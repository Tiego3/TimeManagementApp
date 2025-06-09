using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TimeManagementApp.Models;

namespace TimeManagementLibrary.Models
{
    public class Module : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int Credits { get; set; }
        public int ClassHoursPerWeek { get; set; }
        public DateTime StartDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int WeeksInSemester { get; set; }

        public ObservableCollection<StudyTimeRecord> StudyTimeRecords { get; set; } = new();

        public Module() { }

        public Module(string code, string name, int credits, int classHoursPerWeek, int weeksInSemester, DateTime startDate)
        {
            Code = code;
            Name = name;
            Credits = credits;
            ClassHoursPerWeek = classHoursPerWeek;
            WeeksInSemester = weeksInSemester;
            StartDate = startDate;
        }

        public double SelfStudyHoursPerWeek =>
            Math.Round(((Credits * 10.0) / WeeksInSemester) - ClassHoursPerWeek, 2);

        public double RemainingHoursThisWeek
        {
            get
            {
                var weekStart = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek + 6) % 7);
                var weekEnd = weekStart.AddDays(7);

                var hoursThisWeek = StudyTimeRecords
                    .Where(r => r.StudyDate >= weekStart && r.StudyDate < weekEnd)
                    .Sum(r => r.HoursSpent);

                return Math.Max(0, Math.Round(SelfStudyHoursPerWeek - hoursThisWeek, 2));
            }
        }

        public int CalculateTotalStudyHours()
        {
            return Credits * 10;
        }

        public void AddStudyRecord(DateTime date, double hours)
        {
            StudyTimeRecords.Add(new StudyTimeRecord
            {
                StudyDate = date,
                HoursSpent = hours,
                ModuleId = Id
            });

            OnPropertyChanged(nameof(RemainingHoursThisWeek));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
