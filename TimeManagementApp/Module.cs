using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace TimeManagementApp
{
    class Module
    {
        private string code;
        private string name;
        private int credits;
        private int classHoursPW;
        private int weeksInSemester;
        private DateTime startDate;
        private double selfStudyHoursPW;
        public List<StudyTimeRecord> StudyTimeRecord { get; set; } = new List<StudyTimeRecord>();
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Credits
        {
            get { return credits; }
            set { credits = value; }
        }

        public int ClassHoursPW
        {
            get { return classHoursPW; }
            set { classHoursPW = value; }
        }

        public int WeeksInSemester
        {
            get { return weeksInSemester; }
            set { weeksInSemester = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public double SelfStudyHoursPerWeek
        {
            get
            {
                selfStudyHoursPW = (Credits * 10.0 / WeeksInSemester) -ClassHoursPW;
                return Math.Round(selfStudyHoursPW, 2);
            }
        }

        public double CalcRemainingStudyHours()
        {
            DateTime currentDate = DateTime.Now;
            int currentWeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            double totalSelfStudyHours = (Credits * 10.0 / WeeksInSemester) - ClassHoursPW;
            double recordedHoursForCurrentWeek = StudyTimeRecord
                .Where(record => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(record.StudyDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) == currentWeekNumber)
                .Sum(record => record.HoursSpent);

            return totalSelfStudyHours - recordedHoursForCurrentWeek;
        }

        public double RemainingSelfStudyHours
        {
            get { return CalcRemainingStudyHours(); }
        }
    }


}
