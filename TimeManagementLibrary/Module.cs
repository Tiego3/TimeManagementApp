using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TimeManagementLibrary
{
    public class Module : INotifyPropertyChanged
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int ClassHoursPW { get; set; }
        public int WeeksInSemester { get; set; }
        public DateTime StartDate { get; set; }
        public int Id { get; set; }
        // Existing properties...

        public int UserId { get; set; }
        public User User { get; set; }

       // public ICollection<StudyTimeRecord> StudyTimeRecords { get; set; }
        private double _selfStudyHoursPerWeek;
        public double SelfStudyHoursPerWeek
        {
            get => _selfStudyHoursPerWeek;
            private set
            {
                _selfStudyHoursPerWeek = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StudyTimeRecord> StudyTimeRecords { get; set; }

        public Module()
        {
            StudyTimeRecords = new ObservableCollection<StudyTimeRecord>();
            StudyTimeRecords.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(RemainingSelfStudyHoursThisWeek));
            };
        }

        public Module(string code, string name, int credits, int classHoursPW, int weeksInSemester, DateTime startDate)
            : this()
        {
            Code = code;
            Name = name;
            Credits = credits;
            ClassHoursPW = classHoursPW;
            WeeksInSemester = weeksInSemester;
            StartDate = startDate;
            CalculateSelfStudyHoursPerWeek();
        }

        public void CalculateSelfStudyHoursPerWeek()
        {
            SelfStudyHoursPerWeek = Math.Round(((Credits * 10.0) / WeeksInSemester) - ClassHoursPW, 2);
        }

        public int CalculateTotalStudyHours() => Credits * 10;

        public double RemainingSelfStudyHoursThisWeek
        {
            get
            {
                var currentWeekStart = GetStartOfCurrentWeek();
                var currentWeekEnd = currentWeekStart.AddDays(7);

                double hoursLoggedThisWeek = StudyTimeRecords
                    .Where(r => r.StudyDate >= currentWeekStart && r.StudyDate < currentWeekEnd)
                    .Sum(r => r.HoursSpent);

                double remaining = SelfStudyHoursPerWeek - hoursLoggedThisWeek;
                return Math.Round(Math.Max(0, remaining), 2);
            }
        }

        private DateTime GetStartOfCurrentWeek()
        {
            var today = DateTime.Today;
            var daysSinceMonday = ((int)today.DayOfWeek + 6) % 7; // Ensure Monday = 0
            return today.AddDays(-daysSinceMonday);
        }

        public void AddStudyRecord(DateTime studyDate, double hours)
        {
            StudyTimeRecords.Add(new StudyTimeRecord { StudyDate = studyDate, HoursSpent = hours });
            OnPropertyChanged(nameof(RemainingSelfStudyHoursThisWeek));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
