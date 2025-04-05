using System;
using System.Collections.Generic;
using System.Text;

namespace TimeManagementApp
{
    class StudyTimeRecord
    {
        private DateTime studyDate;
        private double hoursSpent;
        public DateTime StudyDate
        {
            get { return studyDate; }
            set { studyDate = value; }
        }

        public double HoursSpent
        {
            get { return hoursSpent; }
            set { hoursSpent = value; }
        }
    }
}
