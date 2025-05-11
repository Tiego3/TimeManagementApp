using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeManagementLibrary
{
    public class Module
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int ClassHoursPW { get; set; }
        public int WeeksInSemester { get; set; }
        public DateTime StartDate { get; set; }
        public double SelfStudyHoursPerWeek { get; set; }
        public List<StudyTimeRecord> StudyTimeRecords { get; set; }

        public Module()
        {
            StudyTimeRecords = new List<StudyTimeRecord>();
        }

        public Module(string code, string name, int credits, int classHoursPW, int weeksInSemester, DateTime startDate)
        {
            Code = code;
            Name = name;
            Credits = credits;
            ClassHoursPW = classHoursPW;
            WeeksInSemester = weeksInSemester;
            StartDate = startDate;
            StudyTimeRecords = new List<StudyTimeRecord>();
            SelfStudyHoursPerWeek = Math.Round(((Credits * 10.0) / WeeksInSemester) - ClassHoursPW, 2);
        }

        public int CalculateStudyHours()
        {
            return Credits * 10;
        }

        public double RemainingSelfStudyHoursThisWeek
        {
            get
            {
                DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime endOfWeek = startOfWeek.AddDays(7);

                double hoursSpentThisWeek = StudyTimeRecords
                    .Where(r => r.StudyDate >= startOfWeek && r.StudyDate < endOfWeek)
                    .Sum(r => r.HoursSpent);

                double remaining = SelfStudyHoursPerWeek - hoursSpentThisWeek;
                return Math.Round(Math.Max(0, remaining), 2);
            }
        }
    }
}
