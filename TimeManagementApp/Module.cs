﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace TimeManagementApp
{
    public class Module
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public int Credits { get; private set; }
        public int ClassHoursPW { get; private set; }
        public int WeeksInSemester { get; private set; }
        public DateTime StartDate { get; private set; }
        public double SelfStudyHoursPerWeek { get; private set; }
        public List<StudyTimeRecord> StudyTimeRecords { get; private set; }

        // Constructor
        public Module(string code, string name, int credits, int classHoursPW, int weeksInSemester, DateTime startDate)
        {
            Code = code;
            Name = name;
            Credits = credits;
            ClassHoursPW = classHoursPW;
            WeeksInSemester = weeksInSemester;
            StartDate = startDate;
            StudyTimeRecords = new List<StudyTimeRecord>();
            SelfStudyHoursPerWeek = Math.Round(((Credits * 10.0)/WeeksInSemester) - ClassHoursPW, 2);
        }

        // Method to calculate remaining study hours
        public double RemainingSelfStudyHoursThisWeek
        {
            get
            {
                DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                DateTime endOfWeek = startOfWeek.AddDays(7);

                double hoursSpentThisWeek = StudyTimeRecords                  
                    .Sum(r => r.HoursSpent);

                double remaining = SelfStudyHoursPerWeek - hoursSpentThisWeek;
                return Math.Round(Math.Max(0, remaining), 2); 
            }
        }
    }
}
