using System;
using System.Collections.Generic;
using System.Text;

namespace TimeManagementLibrary
{
    /// <summary>
    /// Represents a record of study time for a specific date.
    /// </summary>

    public class StudyTimeRecord
    {
        public DateTime StudyDate { get; set; }
        public double HoursSpent { get; set; }
    }

}
